using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Product.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SellerProducts_ProductId",
                table: "SellerProducts");

            migrationBuilder.DropIndex(
                name: "IX_SellerProducts_SellerId",
                table: "SellerProducts");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProducts_ProductId",
                table: "SellerProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SellerProducts_ProductId",
                table: "SellerProducts");

            migrationBuilder.CreateIndex(
                name: "IX_SellerProducts_ProductId",
                table: "SellerProducts",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerProducts_SellerId",
                table: "SellerProducts",
                column: "SellerId",
                unique: true);
        }
    }
}
