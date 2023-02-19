using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_DocumentFolders_DocumentFolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_DocumentTypes_DocumentTypeId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentFolders",
                table: "DocumentFolders");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "FileTypes");

            migrationBuilder.RenameTable(
                name: "DocumentFolders",
                newName: "FileFolders");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Name",
                table: "FileTypes",
                newName: "IX_FileTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_Code",
                table: "FileTypes",
                newName: "IX_FileTypes_Code");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFolders_Name",
                table: "FileFolders",
                newName: "IX_FileFolders_Name");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFolders_Code",
                table: "FileFolders",
                newName: "IX_FileFolders_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileFolders",
                table: "FileFolders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileFolders_DocumentFolderId",
                table: "Files",
                column: "DocumentFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileTypes_DocumentTypeId",
                table: "Files",
                column: "DocumentTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileFolders_DocumentFolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileTypes_DocumentTypeId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileFolders",
                table: "FileFolders");

            migrationBuilder.RenameTable(
                name: "FileTypes",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "FileFolders",
                newName: "DocumentFolders");

            migrationBuilder.RenameIndex(
                name: "IX_FileTypes_Name",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_FileTypes_Code",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_Code");

            migrationBuilder.RenameIndex(
                name: "IX_FileFolders_Name",
                table: "DocumentFolders",
                newName: "IX_DocumentFolders_Name");

            migrationBuilder.RenameIndex(
                name: "IX_FileFolders_Code",
                table: "DocumentFolders",
                newName: "IX_DocumentFolders_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentFolders",
                table: "DocumentFolders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_DocumentFolders_DocumentFolderId",
                table: "Files",
                column: "DocumentFolderId",
                principalTable: "DocumentFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_DocumentTypes_DocumentTypeId",
                table: "Files",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
