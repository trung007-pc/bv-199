using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SendingFiles_Users_ReceiverId",
                table: "SendingFiles");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "SendingFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SendingFiles_SenderId",
                table: "SendingFiles",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SendingFiles_Users_ReceiverId",
                table: "SendingFiles",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SendingFiles_Users_SenderId",
                table: "SendingFiles",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SendingFiles_Users_ReceiverId",
                table: "SendingFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SendingFiles_Users_SenderId",
                table: "SendingFiles");

            migrationBuilder.DropIndex(
                name: "IX_SendingFiles_SenderId",
                table: "SendingFiles");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "SendingFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_SendingFiles_Users_ReceiverId",
                table: "SendingFiles",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
