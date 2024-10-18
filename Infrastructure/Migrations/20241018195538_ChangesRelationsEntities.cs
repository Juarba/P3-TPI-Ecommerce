using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangesRelationsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Users_clientId",
                table: "SaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "SaleOrders",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrders_clientId",
                table: "SaleOrders",
                newName: "IX_SaleOrders_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "SaleOrderId",
                table: "SaleOrderDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Users_ClientId",
                table: "SaleOrders",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Users_ClientId",
                table: "SaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "SaleOrders",
                newName: "clientId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrders_ClientId",
                table: "SaleOrders",
                newName: "IX_SaleOrders_clientId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SaleOrderId",
                table: "SaleOrderDetails",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Products",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Users_clientId",
                table: "SaleOrders",
                column: "clientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
