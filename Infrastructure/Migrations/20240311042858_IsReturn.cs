using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Recieved_StartedMission",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturn",
                table: "Recieved_SpeedAndTemprature",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturn",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Recieved_StartedMission",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
