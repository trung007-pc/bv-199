using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_Name",
                table: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MeetingContents_Name",
                table: "MeetingContents");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_Name",
                table: "WorkSchedules",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingContents_Name",
                table: "MeetingContents",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_Name",
                table: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MeetingContents_Name",
                table: "MeetingContents");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_Name",
                table: "WorkSchedules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingContents_Name",
                table: "MeetingContents",
                column: "Name",
                unique: true);
        }
    }
}
