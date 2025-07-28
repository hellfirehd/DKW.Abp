using Volo.Abp.Emailing;

namespace Dkw.Abp.Emailing;

public interface IRequireEmailSender
{
    void SetSender(IEmailSender sender);
}
