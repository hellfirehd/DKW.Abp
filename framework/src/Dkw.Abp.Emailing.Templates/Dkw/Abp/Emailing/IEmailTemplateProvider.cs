namespace Dkw.Abp.Emailing;

public interface IEmailTemplateProvider
{
    T GetTemplate<T>() where T : IEmailTemplateBuilder;
}
