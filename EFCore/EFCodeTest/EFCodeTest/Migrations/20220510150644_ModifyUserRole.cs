using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCodeTest.Migrations
{
    public partial class ModifyUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_SysUser_UserId",
                table: "UserRole");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_SysUser_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_SysUser_UserId",
                table: "UserRole");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_SysUser_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
