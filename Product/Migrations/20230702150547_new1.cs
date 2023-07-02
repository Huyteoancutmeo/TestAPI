using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_product_ProductsProductID",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_ProductsProductID",
                table: "category");

            migrationBuilder.DropColumn(
                name: "ProductsProductID",
                table: "category");

            migrationBuilder.CreateIndex(
                name: "IX_product_CategoryID",
                table: "product",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_CategoryID",
                table: "product",
                column: "CategoryID",
                principalTable: "category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_CategoryID",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_CategoryID",
                table: "product");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductID",
                table: "category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_category_ProductsProductID",
                table: "category",
                column: "ProductsProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_category_product_ProductsProductID",
                table: "category",
                column: "ProductsProductID",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
