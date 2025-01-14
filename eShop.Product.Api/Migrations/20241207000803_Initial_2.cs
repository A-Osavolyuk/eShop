using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Product.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerProducts_Sellers_SellerId",
                table: "SellerProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerProducts",
                table: "SellerProducts");

            migrationBuilder.RenameTable(
                name: "SellerProducts",
                newName: "SellerProductsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_SellerProducts_ProductId",
                table: "SellerProductsEntity",
                newName: "IX_SellerProductsEntity_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerProductsEntity",
                table: "SellerProductsEntity",
                columns: new[] { "SellerId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProductsEntity_Products_ProductId",
                table: "SellerProductsEntity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProductsEntity_Sellers_SellerId",
                table: "SellerProductsEntity",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProductsEntity_Products_ProductId",
                table: "SellerProductsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerProductsEntity_Sellers_SellerId",
                table: "SellerProductsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerProductsEntity",
                table: "SellerProductsEntity");

            migrationBuilder.RenameTable(
                name: "SellerProductsEntity",
                newName: "SellerProducts");

            migrationBuilder.RenameIndex(
                name: "IX_SellerProductsEntity_ProductId",
                table: "SellerProducts",
                newName: "IX_SellerProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerProducts",
                table: "SellerProducts",
                columns: new[] { "SellerId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProducts_Sellers_SellerId",
                table: "SellerProducts",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
