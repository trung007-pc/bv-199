using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartment_Departments_DepartmentId",
                table: "UserDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartment_Users_UserId",
                table: "UserDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDepartment",
                table: "UserDepartment");

            migrationBuilder.RenameTable(
                name: "UserDepartment",
                newName: "UserDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_UserDepartment_UserId",
                table: "UserDepartments",
                newName: "IX_UserDepartments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDepartment_DepartmentId",
                table: "UserDepartments",
                newName: "IX_UserDepartments_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDepartments",
                table: "UserDepartments",
                columns: new[] { "Id", "DepartmentId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartments_Departments_DepartmentId",
                table: "UserDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartments_Users_UserId",
                table: "UserDepartments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartments_Departments_DepartmentId",
                table: "UserDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartments_Users_UserId",
                table: "UserDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDepartments",
                table: "UserDepartments");

            migrationBuilder.RenameTable(
                name: "UserDepartments",
                newName: "UserDepartment");

            migrationBuilder.RenameIndex(
                name: "IX_UserDepartments_UserId",
                table: "UserDepartment",
                newName: "IX_UserDepartment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDepartments_DepartmentId",
                table: "UserDepartment",
                newName: "IX_UserDepartment_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDepartment",
                table: "UserDepartment",
                columns: new[] { "Id", "DepartmentId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartment_Departments_DepartmentId",
                table: "UserDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartment_Users_UserId",
                table: "UserDepartment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
