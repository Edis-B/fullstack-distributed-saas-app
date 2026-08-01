using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexiScan.Subscriptions.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedMonthlyToDailyColumnScans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Features_MaxMonthlyScans",
                table: "SubscriptionPlans",
                newName: "Features_MaxDailyScans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Features_MaxDailyScans",
                table: "SubscriptionPlans",
                newName: "Features_MaxMonthlyScans");
        }
    }
}
