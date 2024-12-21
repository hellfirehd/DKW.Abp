using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tenant_Id = table.Column<Guid>(type: "TEXT", nullable: true),
                    CityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<String>(type: "TEXT", nullable: false),
                    Age = table.Column<Int32>(type: "INTEGER", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastActive = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastActiveTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 270, DateTimeKind.Local).AddTicks(6278)),
                    HasDefaultValue = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 12, 13, 15, 11, 42, 271, DateTimeKind.Local).AddTicks(7046)),
                    EntityVersion = table.Column<Int32>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Is_Deleted = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPhones",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<String>(type: "TEXT", nullable: false),
                    Type = table.Column<Int32>(type: "INTEGER", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPhones", x => new { x.PersonId, x.Number });
                    table.ForeignKey(
                        name: "FK_AppPhones_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPhones");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
