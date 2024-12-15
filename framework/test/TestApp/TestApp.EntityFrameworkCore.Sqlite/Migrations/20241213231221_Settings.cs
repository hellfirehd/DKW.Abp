using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Settings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Disable foreign key constraints temporarily
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;", suppressTransaction: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 210, DateTimeKind.Local).AddTicks(978),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 270, DateTimeKind.Local).AddTicks(6278));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 211, DateTimeKind.Local).AddTicks(1883),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 271, DateTimeKind.Local).AddTicks(7046));

            migrationBuilder.CreateTable(
                name: "AbpSettingDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "INTEGER", nullable: false),
                    Providers = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    IsInherited = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettingDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettingDefinitions_Name",
                table: "AbpSettingDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
                table: "AbpSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);

            // Re-enable foreign key constraints
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;", suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpSettingDefinitions");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 270, DateTimeKind.Local).AddTicks(6278),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 210, DateTimeKind.Local).AddTicks(978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 271, DateTimeKind.Local).AddTicks(7046),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 211, DateTimeKind.Local).AddTicks(1883));
        }
    }
}
