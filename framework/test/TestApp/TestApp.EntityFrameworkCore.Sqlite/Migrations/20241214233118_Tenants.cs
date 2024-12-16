using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Tenants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 169, DateTimeKind.Local).AddTicks(7849),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 30, 54, 757, DateTimeKind.Local).AddTicks(8001));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 170, DateTimeKind.Local).AddTicks(7847),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 30, 54, 758, DateTimeKind.Local).AddTicks(9096));

            migrationBuilder.CreateTable(
                name: "AbpTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    NormalizedName = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    EntityVersion = table.Column<Int32>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    Value = table.Column<String>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpTenantConnectionStrings_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_Name",
                table: "AbpTenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants",
                column: "NormalizedName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 30, 54, 757, DateTimeKind.Local).AddTicks(8001),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 169, DateTimeKind.Local).AddTicks(7849));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 30, 54, 758, DateTimeKind.Local).AddTicks(9096),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 170, DateTimeKind.Local).AddTicks(7847));
        }
    }
}
