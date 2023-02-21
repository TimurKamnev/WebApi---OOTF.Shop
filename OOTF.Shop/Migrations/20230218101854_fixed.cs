using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTF.Shopping.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Shop_ShopId",
                table: "User",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
