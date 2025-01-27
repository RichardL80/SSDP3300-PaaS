using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaaS.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "DeliveryMethodId", "OrderDate", "PaymentMethodId", "StatusId", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 27, 11, 47, 50, 646, DateTimeKind.Local).AddTicks(7736), 1, 1, 100m, 1 },
                    { 2, 2, new DateTime(2025, 1, 27, 11, 47, 50, 651, DateTimeKind.Local).AddTicks(1878), 2, 2, 200m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 2);
        }
    }
}
