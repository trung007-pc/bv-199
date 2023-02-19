using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_IssuingAgencies_IssuingAgencyId",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_DocumentTypes_FileTypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileTypeId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_IssuingAgencyId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "FileTypeId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IssuingAgencyId",
                table: "DocumentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Files_DocumentTypeId",
                table: "Files",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_DocumentTypes_DocumentTypeId",
                table: "Files",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_DocumentTypes_DocumentTypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_DocumentTypeId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "FileTypeId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IssuingAgencyId",
                table: "DocumentTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileTypeId",
                table: "Files",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_IssuingAgencyId",
                table: "DocumentTypes",
                column: "IssuingAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_IssuingAgencies_IssuingAgencyId",
                table: "DocumentTypes",
                column: "IssuingAgencyId",
                principalTable: "IssuingAgencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_DocumentTypes_FileTypeId",
                table: "Files",
                column: "FileTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_CreatedBy",
                table: "Files",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
