using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyList.Migrations
{
    /// <inheritdoc />
    public partial class AddHouseholdMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Households_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Households_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId",
                principalTable: "Households",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Households_HouseholdId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Households_HouseholdId",
                table: "AspNetUsers",
                column: "HouseholdId",
                principalTable: "Households",
                principalColumn: "Id");
        }
    }
}
