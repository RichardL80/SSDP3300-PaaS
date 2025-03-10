using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaaS.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentAndDeliveryMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryMethod",
                columns: new[] { "DeliveryMethodId", "MethodName" },
                values: new object[,]
                {
                    { 1, "Pickup" },
                    { 2, "Delivery" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "PaymentMethodId", "MethodName" },
                values: new object[] { 1, "PayPal" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryMethod",
                keyColumn: "DeliveryMethodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryMethod",
                keyColumn: "DeliveryMethodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethod",
                keyColumn: "PaymentMethodId",
                keyValue: 1);
        }
    }
}
