using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace DKW.Abp.OpenIddict;

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