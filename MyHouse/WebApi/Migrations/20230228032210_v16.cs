using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingContents_Users_CreateBy",
                table: "MeetingContents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Users_CreateBy",
                table: "WorkSchedules");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "WorkSchedules",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_CreateBy",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "MeetingContents",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingContents_CreateBy",
                table: "MeetingContents",
                newName: "IX_MeetingContents_CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingContents_Users_CreatedBy",
                table: "MeetingContents",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Users_CreatedBy",
                table: "WorkSchedules",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingContents_Users_CreatedBy",
                table: "MeetingContents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Users_CreatedBy",
                table: "WorkSchedules");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "WorkSchedules",
                newName: "CreateBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_CreatedBy",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_CreateBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "MeetingContents",
                newName: "CreateBy");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingContents_CreatedBy",
                table: "MeetingContents",
                newName: "IX_MeetingContents_CreateBy");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingContents_Users_CreateBy",
                table: "MeetingContents",
                column: "CreateBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Users_CreateBy",
                table: "WorkSchedules",
                column: "CreateBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
