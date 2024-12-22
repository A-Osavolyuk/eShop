using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCodeFieldType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("0c193a62-62f8-4f6f-8363-a967813f0f0f"));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f8552b0-d1f0-404c-85fe-dc3cf056f3db", "44b1337e-57b7-440e-8228-3ca66bc3a65e" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("35b057dd-e5e1-4d5b-881f-65f2425e89e0"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("35b057dd-e5e1-4d5b-881f-65f2425e89e0"));

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Codes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
