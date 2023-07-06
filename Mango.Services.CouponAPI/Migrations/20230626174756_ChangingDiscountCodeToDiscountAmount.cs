using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangingDiscountCodeToDiscountAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountCode",
                table: "Coupons",
                newName: "DiscountAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "Coupons",
                newName: "DiscountCode");
        }
    }
}
