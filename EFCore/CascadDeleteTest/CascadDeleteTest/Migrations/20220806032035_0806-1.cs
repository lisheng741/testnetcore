using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CascadDeleteTest.Migrations
{
    public partial class _08061 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blog_PersonId",
                table: "Blog");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_PersonId",
                table: "Blog",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blog_PersonId",
                table: "Blog");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_PersonId",
                table: "Blog",
                column: "PersonId",
                unique: true);
        }
    }
}
