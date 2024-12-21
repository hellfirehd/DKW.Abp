using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 14, 41, 44, 848, DateTimeKind.Local).AddTicks(924),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 451, DateTimeKind.Local).AddTicks(8377));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 14, 41, 44, 849, DateTimeKind.Local).AddTicks(1732),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 453, DateTimeKind.Local).AddTicks(76));

            migrationBuilder.CreateTable(
                name: "AbpFeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    DisplayName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    ParentName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: true),
                    DisplayName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    DefaultValue = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    IsVisibleToClients = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    IsAvailableToHost = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    AllowedProviders = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    ValueType = table.Column<String>(type: "TEXT", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderName = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureValues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureGroups_Name",
                table: "AbpFeatureGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_GroupName",
                table: "AbpFeatures",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_Name",
                table: "AbpFeatures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureValues_Name_ProviderName_ProviderKey",
                table: "AbpFeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpFeatureGroups");

            migrationBuilder.DropTable(
                name: "AbpFeatures");

            migrationBuilder.DropTable(
                name: "AbpFeatureValues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 451, DateTimeKind.Local).AddTicks(8377),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 14, 41, 44, 848, DateTimeKind.Local).AddTicks(924));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 13, 32, 453, DateTimeKind.Local).AddTicks(76),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 14, 41, 44, 849, DateTimeKind.Local).AddTicks(1732));
        }
    }
}
