using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DocumentFiles_DestinationCode",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DestinationCode",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentFileId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DocumentFileId",
                table: "Notifications",
                column: "DocumentFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DocumentFiles_DocumentFileId",
                table: "Notifications",
                column: "DocumentFileId",
                principalTable: "DocumentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DocumentFiles_DocumentFileId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DocumentFileId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DocumentFileId",
                table: "Notifications");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DestinationCode",
                table: "Notifications",
                column: "DestinationCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DocumentFiles_DestinationCode",
                table: "Notifications",
                column: "DestinationCode",
                principalTable: "DocumentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
