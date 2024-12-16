using Volo.Abp.Settings;

namespace TestApp.Settings;

public class TestAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        context.Add(new SettingDefinition(TestAppSettings.MySetting1));
    }
}
