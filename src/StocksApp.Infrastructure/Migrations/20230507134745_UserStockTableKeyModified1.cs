using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserStockTableKeyModified1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks");

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "UserStocks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "UserStocks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks",
                columns: new[] { "UserID", "StockSymbol", "StockName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks");

            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "UserStocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StockSymbol",
                table: "UserStocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserStocks",
                table: "UserStocks",
                column: "UserID");
        }
    }
}
