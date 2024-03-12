using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsValidAddressInRecieved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValidAddress",
                table: "Recieved_StartedMission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidAddress",
                table: "Recieved_SpeedAndTemprature",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidAddress",
                table: "Recieved_Number",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidAddress",
                table: "Recieved_FinishedMission",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidAddress",
                table: "Recieved_StartedMission");

            migrationBuilder.DropColumn(
                name: "IsValidAddress",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropColumn(
                name: "IsValidAddress",
                table: "Recieved_Number");

            migrationBuilder.DropColumn(
                name: "IsValidAddress",
                table: "Recieved_FinishedMission");
        }
    }
}
