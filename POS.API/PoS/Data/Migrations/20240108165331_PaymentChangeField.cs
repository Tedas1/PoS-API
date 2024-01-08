using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Data.Migrations
{
    public partial class PaymentChangeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Change",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Change",
                table: "Payment");
        }
    }
}
