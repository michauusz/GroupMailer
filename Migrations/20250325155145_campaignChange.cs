using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupMailer.Migrations
{
    /// <inheritdoc />
    public partial class campaignChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EmailCampaigns_EmailCampaignId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EmailCampaignId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EmailCampaignId",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "TargetGroupsId",
                table: "EmailCampaigns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetGroupsId",
                table: "EmailCampaigns");

            migrationBuilder.AddColumn<int>(
                name: "EmailCampaignId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EmailCampaignId",
                table: "Groups",
                column: "EmailCampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EmailCampaigns_EmailCampaignId",
                table: "Groups",
                column: "EmailCampaignId",
                principalTable: "EmailCampaigns",
                principalColumn: "Id");
        }
    }
}
