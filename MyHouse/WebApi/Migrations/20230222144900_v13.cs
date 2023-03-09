using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SendingFiles",
                table: "SendingFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SendingFiles",
                table: "SendingFiles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SendingFiles",
                table: "SendingFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SendingFiles",
                table: "SendingFiles",
                columns: new[] { "Id", "FileId", "ReceiverId" });
        }
    }
}
