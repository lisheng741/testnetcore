using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCodeTest.Migrations
{
    public partial class ModifyUserDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRole_SysUser_UserId",
                table: "SysRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetail_SysUser_UserId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_SysRole_UserId",
                table: "SysRole");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SysRole");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetail_SysUser_UserId",
                table: "UserDetail",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetail_SysUser_UserId",
                table: "UserDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SysRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_UserId",
                table: "SysRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRole_SysUser_UserId",
                table: "SysRole",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetail_SysUser_UserId",
                table: "UserDetail",
                column: "UserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
