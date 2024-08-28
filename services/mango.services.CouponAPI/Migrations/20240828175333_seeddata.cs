using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mango.services.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_coupons",
                table: "coupons");

            migrationBuilder.RenameTable(
                name: "coupons",
                newName: "Coupons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "couponID");

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "couponID", "CouponCode", "DiscountAmount", "LastUpdated", "MinAmount" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.0, new DateTime(2024, 8, 28, 17, 53, 32, 766, DateTimeKind.Utc).AddTicks(2170), 20 },
                    { 2, "20OFF", 20.0, new DateTime(2024, 8, 28, 17, 53, 32, 766, DateTimeKind.Utc).AddTicks(2180), 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "couponID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "couponID",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "coupons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coupons",
                table: "coupons",
                column: "couponID");
        }
    }
}
