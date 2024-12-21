using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class OpenIddict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 39, 959, DateTimeKind.Local).AddTicks(4214),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 169, DateTimeKind.Local).AddTicks(7849));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 39, 960, DateTimeKind.Local).AddTicks(4331),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 170, DateTimeKind.Local).AddTicks(7847));

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationType = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientId = table.Column<String>(type: "TEXT", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<String>(type: "TEXT", nullable: true),
                    ClientType = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    ConsentType = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    DisplayName = table.Column<String>(type: "TEXT", nullable: true),
                    DisplayNames = table.Column<String>(type: "TEXT", nullable: true),
                    JsonWebKeySet = table.Column<String>(type: "TEXT", nullable: true),
                    Permissions = table.Column<String>(type: "TEXT", nullable: true),
                    PostLogoutRedirectUris = table.Column<String>(type: "TEXT", nullable: true),
                    Properties = table.Column<String>(type: "TEXT", nullable: true),
                    RedirectUris = table.Column<String>(type: "TEXT", nullable: true),
                    Requirements = table.Column<String>(type: "TEXT", nullable: true),
                    Settings = table.Column<String>(type: "TEXT", nullable: true),
                    ClientUri = table.Column<String>(type: "TEXT", nullable: true),
                    LogoUri = table.Column<String>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<String>(type: "TEXT", nullable: true),
                    Descriptions = table.Column<String>(type: "TEXT", nullable: true),
                    DisplayName = table.Column<String>(type: "TEXT", nullable: true),
                    DisplayNames = table.Column<String>(type: "TEXT", nullable: true),
                    Name = table.Column<String>(type: "TEXT", maxLength: 200, nullable: true),
                    Properties = table.Column<String>(type: "TEXT", nullable: true),
                    Resources = table.Column<String>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Properties = table.Column<String>(type: "TEXT", nullable: true),
                    Scopes = table.Column<String>(type: "TEXT", nullable: true),
                    Status = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    Subject = table.Column<String>(type: "TEXT", maxLength: 400, nullable: true),
                    Type = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Payload = table.Column<String>(type: "TEXT", nullable: true),
                    Properties = table.Column<String>(type: "TEXT", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReferenceId = table.Column<String>(type: "TEXT", maxLength: 100, nullable: true),
                    Status = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    Subject = table.Column<String>(type: "TEXT", maxLength: 400, nullable: true),
                    Type = table.Column<String>(type: "TEXT", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 169, DateTimeKind.Local).AddTicks(7849),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 39, 959, DateTimeKind.Local).AddTicks(4214));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 14, 15, 31, 18, 170, DateTimeKind.Local).AddTicks(7847),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 14, 15, 31, 39, 960, DateTimeKind.Local).AddTicks(4331));
        }
    }
}
