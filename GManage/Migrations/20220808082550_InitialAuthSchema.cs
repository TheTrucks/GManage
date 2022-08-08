using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GManage.Migrations
{
    public partial class InitialAuthSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateSequence<int>(
                name: "auth.resource_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "auth.user_id_seq");

            migrationBuilder.CreateTable(
                name: "service",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"auth.resource_id_seq\"')"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"auth.user_id_seq\"')"),
                    Login = table.Column<string>(type: "text", nullable: false),
                    pass_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "resource_user",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ResourceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_user", x => new { x.UserId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_resource_user_service_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "auth",
                        principalTable: "service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resource_user_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session",
                schema: "auth",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    session_id = table.Column<string>(type: "text", nullable: false),
                    client_addr = table.Column<string>(type: "text", nullable: false),
                    accessed_at = table.Column<DateTime>(type: "timestamp without timezone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session", x => new { x.user_id, x.session_id });
                    table.ForeignKey(
                        name: "FK_session_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_resource_user_ResourceId",
                schema: "auth",
                table: "resource_user",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "resource_user",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "session",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "service",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "user",
                schema: "auth");

            migrationBuilder.DropSequence(
                name: "auth.resource_id_seq");

            migrationBuilder.DropSequence(
                name: "auth.user_id_seq");
        }
    }
}
