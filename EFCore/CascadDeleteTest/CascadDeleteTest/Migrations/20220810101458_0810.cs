using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CascadDeleteTest.Migrations
{
    public partial class _0810 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                type: "char(36)",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(string),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Role",
                type: "char(36)",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(string),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                comment: "标题",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Post",
                type: "char(36)",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(string),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Person",
                type: "char(36)",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(string),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true,
                comment: "博客名",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Blog",
                type: "char(36)",
                nullable: false,
                comment: "主键",
                oldClrType: typeof(string),
                oldType: "char(36)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldComment: "主键");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Role",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldComment: "主键");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "标题");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Post",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldComment: "主键");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Person",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldComment: "主键");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "博客名");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Blog",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldComment: "主键");
        }
    }
}
