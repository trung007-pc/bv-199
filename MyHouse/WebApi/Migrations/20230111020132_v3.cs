using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeletion",
                table: "Parts",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeletion",
                table: "PartReviews",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "PartReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PartReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "PartReviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "PartReviews");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PartReviews");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "PartReviews");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Parts",
                newName: "IsDeletion");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "PartReviews",
                newName: "IsDeletion");
        }
    }
}
