﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace TestApp.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 769, DateTimeKind.Local).AddTicks(9950),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 210, DateTimeKind.Local).AddTicks(978));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 771, DateTimeKind.Local).AddTicks(995),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 211, DateTimeKind.Local).AddTicks(1883));

            migrationBuilder.CreateTable(
                name: "AbpClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    Required = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    IsStatic = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    Regex = table.Column<String>(type: "TEXT", maxLength: 512, nullable: true),
                    RegexDescription = table.Column<String>(type: "TEXT", maxLength: 128, nullable: true),
                    Description = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    ValueType = table.Column<Int32>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ParentId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Code = table.Column<String>(type: "TEXT", maxLength: 95, nullable: false),
                    DisplayName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
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
                    table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    IsDefault = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    IsStatic = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    IsPublic = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    EntityVersion = table.Column<Int32>(type: "INTEGER", nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ApplicationName = table.Column<String>(type: "TEXT", maxLength: 96, nullable: true),
                    Identity = table.Column<String>(type: "TEXT", maxLength: 96, nullable: true),
                    Action = table.Column<String>(type: "TEXT", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    TenantName = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    ClientId = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<String>(type: "TEXT", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<String>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SessionId = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    Device = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    DeviceInfo = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientId = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    IpAddresses = table.Column<String>(type: "TEXT", maxLength: 2048, nullable: true),
                    SignedIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExtraProperties = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserDelegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    Surname = table.Column<String>(type: "TEXT", maxLength: 64, nullable: true),
                    Email = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<String>(type: "TEXT", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    IsExternal = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<String>(type: "TEXT", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    IsActive = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<Boolean>(type: "INTEGER", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<Int32>(type: "INTEGER", nullable: false, defaultValue: 0),
                    ShouldChangePasswordOnNextLogin = table.Column<Boolean>(type: "INTEGER", nullable: false),
                    EntityVersion = table.Column<Int32>(type: "INTEGER", nullable: false),
                    LastPasswordChangeTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_AbpUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ClaimType = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<String>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ClaimType = table.Column<String>(type: "TEXT", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<String>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserClaims_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProviderKey = table.Column<String>(type: "TEXT", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<String>(type: "TEXT", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_AbpUserLogins_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<String>(type: "TEXT", maxLength: 64, nullable: false),
                    Name = table.Column<String>(type: "TEXT", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Value = table.Column<String>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpUserTokens_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId",
                table: "AbpLinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "AbpOrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_Code",
                table: "AbpOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_NormalizedName",
                table: "AbpRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Action",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_ApplicationName",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Identity",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_UserId",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_Device",
                table: "AbpSessions",
                column: "Device");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_SessionId",
                table: "AbpSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_TenantId_UserId",
                table: "AbpSessions",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "AbpUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_RoleId_UserId",
                table: "AbpUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_Email",
                table: "AbpUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedEmail",
                table: "AbpUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedUserName",
                table: "AbpUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserName",
                table: "AbpUsers",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpClaimTypes");

            migrationBuilder.DropTable(
                name: "AbpLinkUsers");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSecurityLogs");

            migrationBuilder.DropTable(
                name: "AbpSessions");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserDelegations");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "AbpUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActiveTime",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 210, DateTimeKind.Local).AddTicks(978),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 769, DateTimeKind.Local).AddTicks(9950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HasDefaultValue",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 13, 15, 12, 21, 211, DateTimeKind.Local).AddTicks(1883),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2024, 12, 13, 15, 12, 56, 771, DateTimeKind.Local).AddTicks(995));
        }
    }
}
