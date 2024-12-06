namespace Inara.Abp.OpenIddict;

public class InaraOpenIddictOptions
{
    public String? EncryptionCertificatePath { get; set; }
    public String? EncryptionCertificateKey { get; set; }
    public String? SigningCertificatePath { get; set; }
    public String? SigningCertificateKey { get; set; }
}