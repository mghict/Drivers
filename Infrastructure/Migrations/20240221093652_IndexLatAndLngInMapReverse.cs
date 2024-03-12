using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IndexLatAndLngInMapReverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                schema: "dbo",
                table: "MapReverse",
                type: "decimal(12,8)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                schema: "dbo",
                table: "MapReverse",
                type: "decimal(12,8)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LatAndLng_MapReverse",
                schema: "dbo",
                table: "MapReverse",
                columns: new[] { "Lat", "Lng" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LatAndLng_MapReverse",
                schema: "dbo",
                table: "MapReverse");

            migrationBuilder.AlterColumn<string>(
                name: "Lng",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)");

            migrationBuilder.AlterColumn<string>(
                name: "Lat",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)");
        }
    }
}
