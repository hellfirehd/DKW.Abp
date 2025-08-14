// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace Dkw.Abp.OpenIddict;

public static class OpenIddictExtensions
{
    public static void UseCertificates(this OpenIddictServerBuilder options, Action<DkwOpenIddictOptions> confiugre)
    {
        var opt = new DkwOpenIddictOptions();

        confiugre?.Invoke(opt);

        // https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html
        // OpenIdDict uses two types of credentials to secure the token it issues.

        // 1. Encryption credentials are used to ensure the content of tokens cannot be read by malicious parties
        if (!String.IsNullOrEmpty(opt.EncryptionCertificatePath) && File.Exists(opt.EncryptionCertificatePath))
        {
            var encryptionKey = X509CertificateLoader.LoadPkcs12FromFile(opt.EncryptionCertificatePath, opt.EncryptionCertificateKey, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.EphemeralKeySet);
            options.AddEncryptionCertificate(encryptionKey);
        }
        else
        {
            options.AddDevelopmentEncryptionCertificate();
        }

        // 2. Signing credentials are used to protect against tampering
        if (!String.IsNullOrEmpty(opt.SigningCertificatePath) && File.Exists(opt.SigningCertificatePath))
        {
            var signingKey = X509CertificateLoader.LoadPkcs12FromFile(opt.SigningCertificatePath, opt.SigningCertificateKey, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.EphemeralKeySet);
            options.AddSigningCertificate(signingKey);
        }
        else
        {
            options.AddDevelopmentSigningCertificate();
        }
    }
}
