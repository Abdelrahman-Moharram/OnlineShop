using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixProductItemOrderItemRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductItemId",
                table: "OrderItem");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductItemId",
                table: "OrderItem",
                column: "ProductItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductItemId",
                table: "OrderItem");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductItemId",
                table: "OrderItem",
                column: "ProductItemId");
        }
    }
}
