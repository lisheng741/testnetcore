using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CascadDeleteTest.Migrations
{
    public partial class _08062 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Blog",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(36)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Blog",
                type: "char(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(36)",
                oldNullable: true);
        }
    }
}
