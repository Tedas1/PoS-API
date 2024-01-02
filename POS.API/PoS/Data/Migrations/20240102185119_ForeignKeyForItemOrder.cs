using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Data.Migrations
{
    public partial class ForeignKeyForItemOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_ItemId",
                table: "ItemOrder",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Item_ItemId",
                table: "ItemOrder",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Order_OrderId",
                table: "ItemOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Item_ItemId",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Order_OrderId",
                table: "ItemOrder");

            migrationBuilder.DropIndex(
                name: "IX_ItemOrder_ItemId",
                table: "ItemOrder");
        }
    }
}
