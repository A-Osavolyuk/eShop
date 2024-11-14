using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialTptStrategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Brands_BrandId",
                table: "Clothing");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Clothing_BrandId",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Clothing");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Products_Id",
                table: "Clothing",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Products_Id",
                table: "Shoes",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Products_Id",
                table: "Clothing");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerProducts_Products_ProductId",
                table: "SellerProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Products_Id",
                table: "Shoes");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Shoes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Shoes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "Clothing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "Clothing",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Clothing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clothing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Clothing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clothing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Clothing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Clothing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothing_BrandId",
                table: "Clothing",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Brands_BrandId",
                table: "Clothing",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Brands_BrandId",
                table: "Shoes",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
