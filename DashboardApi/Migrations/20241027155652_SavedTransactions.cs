using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashboardApi.Migrations
{
    /// <inheritdoc />
    public partial class SavedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Indicator",
                table: "Transactions",
                newName: "indicator");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transactions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Transactions",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "Transactions",
                newName: "booking_date");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "indicator",
                table: "Transactions",
                newName: "Indicator");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Transactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Transactions",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "booking_date",
                table: "Transactions",
                newName: "BookingDate");
        }
    }
}
