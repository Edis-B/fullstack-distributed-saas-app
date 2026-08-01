using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexiScan.Subscriptions.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlanFeaturesToPlansTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Features_AllowApiAccess",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Features_AllowCustomDomains",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Features_AllowCustomLogos",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Features_AllowTrackingPixels",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Features_HasAdvancedAnalytics",
                table: "SubscriptionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Features_MaxActiveCodes",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Features_MaxMonthlyScans",
                table: "SubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Features_AllowApiAccess",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_AllowCustomDomains",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_AllowCustomLogos",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_AllowTrackingPixels",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_HasAdvancedAnalytics",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_MaxActiveCodes",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "Features_MaxMonthlyScans",
                table: "SubscriptionPlans");
        }
    }
}
