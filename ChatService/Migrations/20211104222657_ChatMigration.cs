using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace СhatService.Migrations
{
    public partial class ChatMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    users = table.Column<List<int>>(type: "integer[]", nullable: true),
                    messages = table.Column<List<int>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    readBy = table.Column<List<int>>(type: "integer[]", nullable: true),
                    repliedFrom = table.Column<List<int>>(type: "integer[]", nullable: true),
                    edited = table.Column<bool>(type: "boolean", nullable: false),
                    usersDelete = table.Column<List<int>>(type: "integer[]", nullable: true),
                    author = table.Column<int>(type: "integer", nullable: false),
                    deletedForAll = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    surname = table.Column<string>(type: "text", nullable: true),
                    roles = table.Column<int[]>(type: "integer[]", nullable: true),
                    Chats = table.Column<List<int>>(type: "integer[]", nullable: true),
                    lastActivity = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    src = table.Column<string>(type: "text", nullable: true),
                    thumbnail = table.Column<string>(type: "text", nullable: true),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    MessageID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attachments_Messages_MessageID",
                        column: x => x.MessageID,
                        principalTable: "Messages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageID",
                table: "Attachments",
                column: "MessageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
