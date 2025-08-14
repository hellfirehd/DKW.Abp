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

using Volo.Abp.Threading;

namespace TestApp;

public static class TestAppModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        /* You can change max lengths for properties of the
         * entities defined in the modules used by your application.
         *
         * Example: Change user and role name max lengths

           AbpUserConsts.MaxNameLength = 99;
           IdentityRoleConsts.MaxNameLength = 99;

         * Notice: It is not suggested to change property lengths
         * unless you really need it. Go with the standard values wherever possible.
         *
         * If you are using EF Core, you will need to run the add-migration command after your changes.
         */
    }

    private static void ConfigureExtraProperties()
    {
        /* You can configure extra properties for the
         * entities defined in the modules used by your application.
         *
         * This class can be used to define these extra properties
         * with a high level, easy to use API.
         *
         * Example: Add a new property to the user entity of the identity module

           ObjectExtensionManager.Instance.Modules()
              .ConfigureIdentity(identity =>
              {
                  identity.ConfigureUser(user =>
                  {
                      user.AddOrUpdateProperty<string>( //property type: string
                          "SocialSecurityNumber", //property name
                          property =>
                          {
                              //validation rules
                              property.Attributes.Add(new RequiredAttribute());
                              property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});
                              
                              property.Configuration[IdentityModuleExtensionConsts.ConfigurationNames.AllowUserToEdit] = true;

                              //...other configurations for this property
                          }
                      );
                  });
              });

         * See the documentation for more:
         * https://docs.abp.io/en/abp/latest/Module-Entity-Extensions
         */
    }
}
