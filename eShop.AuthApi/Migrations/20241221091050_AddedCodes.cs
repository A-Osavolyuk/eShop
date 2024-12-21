using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.AuthApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("99be06c5-8f0a-4915-90ea-2d84b16e83a8"));

            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    CodeType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Codes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Codes_UserId",
                table: "Codes",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Codes");

            migrationBuilder.DeleteData(
                table: "PersonalData",
                keyColumn: "Id",
                keyValue: new Guid("16f71363-7aed-42ce-b7c3-710e48e9914e"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f090ddde-f3a5-4f07-98eb-f3706ecb0624", "4e91e37b-a543-47cc-afb3-e18a43451fa6" });

            migrationBuilder.InsertData(
                table: "PersonalData",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "UserId" },
                values: new object[] { new Guid("99be06c5-8f0a-4915-90ea-2d84b16e83a8"), new DateTime(2004, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexander", "Male", "Osavolyuk", "abb9d2ed-c3d2-4df9-ba88-eab018b95bc3" });
        }
    }
}
