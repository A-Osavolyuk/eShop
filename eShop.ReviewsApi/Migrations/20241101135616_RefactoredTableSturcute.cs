using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.ReviewsApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredTableSturcute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Reviews_CommentId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Comments",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Comments",
                newName: "ProductId");

            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Comments",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comments",
                newName: "ReviewId");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Comments",
                newName: "Text");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Reviews_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
