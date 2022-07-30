using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTest.Migrations
{
    public partial class _07301 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestGuid");

            migrationBuilder.DropTable(
                name: "TestGuidString");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Id",
            //    table: "TestGuidString",
            //    type: "char(36)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Id",
            //    table: "TestGuid",
            //    type: "char(36)",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Id",
            //    table: "TestGuidString",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "char(36)");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "TestGuid",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "char(36)");
        }
    }
}
