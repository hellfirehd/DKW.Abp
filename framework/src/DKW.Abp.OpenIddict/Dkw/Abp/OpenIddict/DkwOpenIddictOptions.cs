namespace DKW.Abp.OpenIddict;

public class DkwOpenIddictOptions
{
    public String? EncryptionCertificatePath { get; set; }
    public String? EncryptionCertificateKey { get; set; }
    public String? SigningCertificatePath { get; set; }
    public String? SigningCertificateKey { get; set; }
}