using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MapReverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AutoModels_AutoBrands_AutoBrandId1",
            //    table: "AutoModels");

            //migrationBuilder.DropIndex(
            //    name: "IX_AutoModels_AutoBrandId1",
            //    table: "AutoModels");

            //migrationBuilder.DropColumn(
            //    name: "AutoBrandId1",
            //    table: "AutoModels");

            migrationBuilder.AddColumn<long>(
                name: "MapReverseId",
                table: "Recieved_StartedMission",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MapReverseId",
                table: "Recieved_SpeedAndTemprature",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MapReverseId",
                table: "Recieved_Number",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MapReverseId",
                table: "Recieved_FinishedMission",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MapReverses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCompact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Primary = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Poi = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Penult = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    RuralDistrict = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Village = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Neighbourhood = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Last = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Plaque = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapReverses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recieved_StartedMission_MapReverseId",
                table: "Recieved_StartedMission",
                column: "MapReverseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recieved_SpeedAndTemprature_MapReverseId",
                table: "Recieved_SpeedAndTemprature",
                column: "MapReverseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recieved_Number_MapReverseId",
                table: "Recieved_Number",
                column: "MapReverseId");

            migrationBuilder.CreateIndex(
                name: "IX_Recieved_FinishedMission_MapReverseId",
                table: "Recieved_FinishedMission",
                column: "MapReverseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_FinishedMission_MapReverses_MapReverseId",
                table: "Recieved_FinishedMission",
                column: "MapReverseId",
                principalTable: "MapReverses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_Number_MapReverses_MapReverseId",
                table: "Recieved_Number",
                column: "MapReverseId",
                principalTable: "MapReverses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_SpeedAndTemprature_MapReverses_MapReverseId",
                table: "Recieved_SpeedAndTemprature",
                column: "MapReverseId",
                principalTable: "MapReverses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_StartedMission_MapReverses_MapReverseId",
                table: "Recieved_StartedMission",
                column: "MapReverseId",
                principalTable: "MapReverses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_FinishedMission_MapReverses_MapReverseId",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_Number_MapReverses_MapReverseId",
                table: "Recieved_Number");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_SpeedAndTemprature_MapReverses_MapReverseId",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_StartedMission_MapReverses_MapReverseId",
                table: "Recieved_StartedMission");

            migrationBuilder.DropTable(
                name: "MapReverses");

            migrationBuilder.DropIndex(
                name: "IX_Recieved_StartedMission_MapReverseId",
                table: "Recieved_StartedMission");

            migrationBuilder.DropIndex(
                name: "IX_Recieved_SpeedAndTemprature_MapReverseId",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropIndex(
                name: "IX_Recieved_Number_MapReverseId",
                table: "Recieved_Number");

            migrationBuilder.DropIndex(
                name: "IX_Recieved_FinishedMission_MapReverseId",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropColumn(
                name: "MapReverseId",
                table: "Recieved_StartedMission");

            migrationBuilder.DropColumn(
                name: "MapReverseId",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropColumn(
                name: "MapReverseId",
                table: "Recieved_Number");

            migrationBuilder.DropColumn(
                name: "MapReverseId",
                table: "Recieved_FinishedMission");

            migrationBuilder.AddColumn<int>(
                name: "AutoBrandId1",
                table: "AutoModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoModels_AutoBrandId1",
                table: "AutoModels",
                column: "AutoBrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoModels_AutoBrands_AutoBrandId1",
                table: "AutoModels",
                column: "AutoBrandId1",
                principalTable: "AutoBrands",
                principalColumn: "Id");
        }
    }
}
