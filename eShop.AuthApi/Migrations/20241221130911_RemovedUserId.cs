using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_AspNetUsers_UserId",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Codes_UserId",
                table: "Codes");

            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("46f76abd-01a5-4978-85d6-9eb7390f0bb8"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Codes");

            migrationBuilder.AddColumn<int>(
                name: "CodeType",
                table: "Codes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SentTo",
                table: "Codes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76ab2d19-549f-42b1-8ac9-7e5e34734bd1", "7d5689e1-6463-44d6-b7a8-2d7c1fb305f1" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("0c193a62-62f8-4f6f-8363-a967813f0f0f"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("0c193a62-62f8-4f6f-8363-a967813f0f0f"));

            migrationBuilder.DropColumn(
                name: "CodeType",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "SentTo",
                table: "Codes");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Codes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5cf51e63-54f5-4b09-be99-0256c93564ed", "ef72f6c6-6978-4066-8eda-de20374e83cf" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("46f76abd-01a5-4978-85d6-9eb7390f0bb8"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });

            migrationBuilder.CreateIndex(
                name: "IX_Codes_UserId",
                table: "Codes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_AspNetUsers_UserId",
                table: "Codes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
