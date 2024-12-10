using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DKW.OpenId.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationType = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<String>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<String>(type: "text", nullable: true),
                    ClientType = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<String>(type: "text", nullable: true),
                    DisplayNames = table.Column<String>(type: "text", nullable: true),
                    JsonWebKeySet = table.Column<String>(type: "text", nullable: true),
                    Permissions = table.Column<String>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<String>(type: "text", nullable: true),
                    Properties = table.Column<String>(type: "text", nullable: true),
                    RedirectUris = table.Column<String>(type: "text", nullable: true),
                    Requirements = table.Column<String>(type: "text", nullable: true),
                    Settings = table.Column<String>(type: "text", nullable: true),
                    ClientUri = table.Column<String>(type: "text", nullable: true),
                    LogoUri = table.Column<String>(type: "text", nullable: true),
                    ExtraProperties = table.Column<String>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<String>(type: "text", nullable: true),
                    Descriptions = table.Column<String>(type: "text", nullable: true),
                    DisplayName = table.Column<String>(type: "text", nullable: true),
                    DisplayNames = table.Column<String>(type: "text", nullable: true),
                    Name = table.Column<String>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<String>(type: "text", nullable: true),
                    Resources = table.Column<String>(type: "text", nullable: true),
                    ExtraProperties = table.Column<String>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Properties = table.Column<String>(type: "text", nullable: true),
                    Scopes = table.Column<String>(type: "text", nullable: true),
                    Status = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<String>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<String>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Payload = table.Column<String>(type: "text", nullable: true),
                    Properties = table.Column<String>(type: "text", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReferenceId = table.Column<String>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<String>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<String>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<String>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<Boolean>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
        }
    }
}