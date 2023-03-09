using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentFolderId",
                table: "DocumentFiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles",
                column: "DocumentFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentFolderId",
                table: "DocumentFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles",
                column: "DocumentFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
