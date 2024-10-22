using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationSaleOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId",
                unique: true);
        }
    }
}
