using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTest.Migrations
{
    public partial class _07303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "TestGuid2",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "char(36)");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "TestGuid1",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "char(36)");

            migrationBuilder.CreateTable(
                name: "TestGuid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateSequential = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestGuid", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestGuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TestGuid2",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TestGuid1",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
