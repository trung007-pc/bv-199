using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_Name",
                table: "DocumentFiles");

            migrationBuilder.RenameColumn(
                name: "IsPrint",
                table: "DocumentFiles",
                newName: "AllowDownloadAndPrint");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_Name",
                table: "DocumentFiles",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles");

            migrationBuilder.DropIndex(
                name: "IX_DocumentFiles_Name",
                table: "DocumentFiles");

            migrationBuilder.RenameColumn(
                name: "AllowDownloadAndPrint",
                table: "DocumentFiles",
                newName: "IsPrint");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_Code",
                table: "DocumentFiles",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_Name",
                table: "DocumentFiles",
                column: "Name",
                unique: true);
        }
    }
}
