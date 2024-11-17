using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DashboardApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "SavedTransactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedTransactions",
                table: "SavedTransactions",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedTransactions",
                table: "SavedTransactions");

            migrationBuilder.RenameTable(
                name: "SavedTransactions",
                newName: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "id");
        }
    }
}
