using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileFolders_DocumentFolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileTypes_DocumentTypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_IssuingAgencies_IssuingAgencyId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_FileVersions_Files_FileId",
                table: "FileVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Files_DestinationCode",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SendingFiles_Files_FileId",
                table: "SendingFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_Code",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "DocumentFiles");

            migrationBuilder.RenameIndex(
                name: "IX_Files_StorageCode",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_StorageCode");

            migrationBuilder.RenameIndex(
                name: "IX_Files_Name",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Files_IssuingAgencyId",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_IssuingAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_DocumentTypeId",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_DocumentFolderId",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_DocumentFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_CreatedBy",
                table: "DocumentFiles",
                newName: "IX_DocumentFiles_CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DocumentFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentFiles",
                table: "DocumentFiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles",
                column: "DocumentFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_FileTypes_DocumentTypeId",
                table: "DocumentFiles",
                column: "DocumentTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_IssuingAgencies_IssuingAgencyId",
                table: "DocumentFiles",
                column: "IssuingAgencyId",
                principalTable: "IssuingAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentFiles_Users_CreatedBy",
                table: "DocumentFiles",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersions_DocumentFiles_FileId",
                table: "FileVersions",
                column: "FileId",
                principalTable: "DocumentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DocumentFiles_DestinationCode",
                table: "Notifications",
                column: "DestinationCode",
                principalTable: "DocumentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendingFiles_DocumentFiles_FileId",
                table: "SendingFiles",
                column: "FileId",
                principalTable: "DocumentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_FileFolders_DocumentFolderId",
                table: "DocumentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_FileTypes_DocumentTypeId",
                table: "DocumentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_IssuingAgencies_IssuingAgencyId",
                table: "DocumentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentFiles_Users_CreatedBy",
                table: "DocumentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_FileVersions_DocumentFiles_FileId",
                table: "FileVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DocumentFiles_DestinationCode",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SendingFiles_DocumentFiles_FileId",
                table: "SendingFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentFiles",
                table: "DocumentFiles");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles");

            migrationBuilder.RenameTable(
                name: "DocumentFiles",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_StorageCode",
                table: "Files",
                newName: "IX_Files_StorageCode");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_Name",
                table: "Files",
                newName: "IX_Files_Name");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_IssuingAgencyId",
                table: "Files",
                newName: "IX_Files_IssuingAgencyId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_DocumentTypeId",
                table: "Files",
                newName: "IX_Files_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_DocumentFolderId",
                table: "Files",
                newName: "IX_Files_DocumentFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentFiles_CreatedBy",
                table: "Files",
                newName: "IX_Files_CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Files",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Code",
                table: "Files",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_IssuingAgencies_IssuingAgencyId",
                table: "Files",
                column: "IssuingAgencyId",
                principalTable: "IssuingAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersions_Files_FileId",
                table: "FileVersions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Files_DestinationCode",
                table: "Notifications",
                column: "DestinationCode",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendingFiles_Files_FileId",
                table: "SendingFiles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
