using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("16f71363-7aed-42ce-b7c3-710e48e9914e"));

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "Codes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9265b0f3-4994-45f9-9d16-7b924b55bc26", "2d4b336b-a3b6-4690-b442-7e4eeb35d390" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("2425850c-691c-4057-95fe-f0cc4986ee56"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("2425850c-691c-4057-95fe-f0cc4986ee56"));

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Codes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "719a5259-fdd6-444f-9c63-a109ac5e42d9", "0f5cad52-be92-4f8f-8350-0c668d3d805e" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("16f71363-7aed-42ce-b7c3-710e48e9914e"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }
    }
}
