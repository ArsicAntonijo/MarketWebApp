using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class ReceiptTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributerFirm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfReceipt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StringItems = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ReceiptId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ReceiptId",
                table: "Item",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Receipt_ReceiptId",
                table: "Item",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "ReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Receipt_ReceiptId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Item_ReceiptId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Item");
        }
    }
}
