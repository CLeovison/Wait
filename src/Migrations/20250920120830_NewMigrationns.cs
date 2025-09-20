using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryName",
                table: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                column: "CategoryName",
                unique: true);
        }
    }
}
