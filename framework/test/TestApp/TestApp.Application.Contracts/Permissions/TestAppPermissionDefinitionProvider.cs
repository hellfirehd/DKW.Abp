using TestApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestApp.Permissions;

public class TestAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestAppPermissions.GroupName);
        //Define your own permissions here. Example:
        myGroup.AddPermission(TestAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(String name)
    {
        return LocalizableString.Create<TestAppResource>(name);
    }
}
