using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTest.Migrations
{
    public partial class _0723 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestDelete",
                type: "nvarchar(max)",
                nullable: true,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestDelete",
                type: "bit",
                nullable: false,
                comment: "软删",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestBusiness",
                type: "nvarchar(max)",
                nullable: true,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBusiness",
                type: "bit",
                nullable: false,
                comment: "软删",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestDelete",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "名称");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestDelete",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "软删");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TestBusiness",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "名称");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBusiness",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "软删");
        }
    }
}
