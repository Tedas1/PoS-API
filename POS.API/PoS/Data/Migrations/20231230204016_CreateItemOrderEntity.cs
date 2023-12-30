using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Data.Migrations
{
    public partial class CreateItemOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Item_ItemsId",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Order_OrdersId",
                table: "ItemOrder");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrder_OrdersId",
                table: "ItemOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "ItemOrder",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "ItemOrder",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Item",
                newName: "PPU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ItemOrder",
                newName: "OrdersId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ItemOrder",
                newName: "ItemsId");

            migrationBuilder.RenameColumn(
                name: "PPU",
                table: "Item",
                newName: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_OrdersId",
                table: "ItemOrder",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Item_ItemsId",
                table: "ItemOrder",
                column: "ItemsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Order_OrdersId",
                table: "ItemOrder",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
