using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace СhatService.Migrations
{
    public partial class main : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Messages_MessageID",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_MessageID",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "MessageID",
                table: "Attachments");

            migrationBuilder.AddColumn<List<int>>(
                name: "attachments",
                table: "Messages",
                type: "integer[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attachments",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageID",
                table: "Attachments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageID",
                table: "Attachments",
                column: "MessageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Messages_MessageID",
                table: "Attachments",
                column: "MessageID",
                principalTable: "Messages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
