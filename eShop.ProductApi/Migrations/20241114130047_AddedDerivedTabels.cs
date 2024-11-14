using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedDerivedTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts");

            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "Products",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "Products",
                newName: "Price");

            migrationBuilder.CreateTable(
                name: "Clothing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothing_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothing_BrandId",
                table: "Clothing",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clothing");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Products",
                newName: "Price_Currency");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "Price_Amount");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
