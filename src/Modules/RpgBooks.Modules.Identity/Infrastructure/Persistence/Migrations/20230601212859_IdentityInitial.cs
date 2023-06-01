using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdentityInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IDT_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDT_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HonorificTitle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email_Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    LastSuccessAccess = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PhoneNumber_Value = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDT_Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDT_Claims_IDT_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IDT_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDT_Claims_IDT_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "IDT_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IDT_LoginRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_LoginRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDT_LoginRecords_IDT_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "IDT_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDT_SecurityTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpirationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TokenType_Value = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_SecurityTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDT_SecurityTokens_IDT_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "IDT_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDT_UsersRoles",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDT_UsersRoles", x => new { x.RolesId, x.UserId });
                    table.ForeignKey(
                        name: "FK_IDT_UsersRoles_IDT_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "IDT_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IDT_UsersRoles_IDT_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "IDT_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IDT_Claims_RoleId",
                table: "IDT_Claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IDT_Claims_UserId",
                table: "IDT_Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDT_LoginRecords_UserId",
                table: "IDT_LoginRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDT_SecurityTokens_UserId",
                table: "IDT_SecurityTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDT_UsersRoles_UserId",
                table: "IDT_UsersRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IDT_Claims");

            migrationBuilder.DropTable(
                name: "IDT_LoginRecords");

            migrationBuilder.DropTable(
                name: "IDT_SecurityTokens");

            migrationBuilder.DropTable(
                name: "IDT_UsersRoles");

            migrationBuilder.DropTable(
                name: "IDT_Roles");

            migrationBuilder.DropTable(
                name: "IDT_Users");
        }
    }
}
