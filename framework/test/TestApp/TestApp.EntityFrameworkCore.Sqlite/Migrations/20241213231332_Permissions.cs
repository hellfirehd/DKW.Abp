using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 451, DateTimeKind.Local).AddTicks(8377),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 769, DateTimeKind.Local).AddTicks(9950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 453, DateTimeKind.Local).AddTicks(76),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 771, DateTimeKind.Local).AddTicks(995));

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderName = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    DisplayName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    ParentName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: true),
                    DisplayName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    MultiTenancySide = table.Column<Byte>(type: "INTEGER", nullable: false),
                    Providers = table.Column<String>(type: "TEXT", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "AbpPermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGroups_Name",
                table: "AbpPermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_GroupName",
                table: "AbpPermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 769, DateTimeKind.Local).AddTicks(9950),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 451, DateTimeKind.Local).AddTicks(8377));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 771, DateTimeKind.Local).AddTicks(995),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 453, DateTimeKind.Local).AddTicks(76));
        }
    }
}
