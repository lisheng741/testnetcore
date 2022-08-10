using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CascadDeleteTest.Migrations
{
    public partial class _0806 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Blog",
                type: "char(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_PersonId",
                table: "Blog",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Person_PersonId",
                table: "Blog",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Person_PersonId",
                table: "Blog");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Blog_PersonId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Blog");
        }
    }
}
