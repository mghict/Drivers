using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MapReverse01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapReverses",
                table: "MapReverses");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "MapReverses",
                newName: "MapReverse",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "Village",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RuralDistrict",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Primary",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Poi",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plaque",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Penult",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Neighbourhood",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                schema: "dbo",
                table: "MapReverse",
                type: "decimal(12,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                schema: "dbo",
                table: "MapReverse",
                type: "decimal(12,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Last",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "District",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "County",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "dbo",
                table: "MapReverse",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapReverse",
                schema: "dbo",
                table: "MapReverse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_FinishedMission_MapReverse_MapReverseId",
                table: "Recieved_FinishedMission",
                column: "MapReverseId",
                principalSchema: "dbo",
                principalTable: "MapReverse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_Number_MapReverse_MapReverseId",
                table: "Recieved_Number",
                column: "MapReverseId",
                principalSchema: "dbo",
                principalTable: "MapReverse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_SpeedAndTemprature_MapReverse_MapReverseId",
                table: "Recieved_SpeedAndTemprature",
                column: "MapReverseId",
                principalSchema: "dbo",
                principalTable: "MapReverse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieved_StartedMission_MapReverse_MapReverseId",
                table: "Recieved_StartedMission",
                column: "MapReverseId",
                principalSchema: "dbo",
                principalTable: "MapReverse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_FinishedMission_MapReverse_MapReverseId",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_Number_MapReverse_MapReverseId",
                table: "Recieved_Number");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_SpeedAndTemprature_MapReverse_MapReverseId",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropForeignKey(
                name: "FK_Recieved_StartedMission_MapReverse_MapReverseId",
                table: "Recieved_StartedMission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapReverse",
                schema: "dbo",
                table: "MapReverse");

            migrationBuilder.RenameTable(
                name: "MapReverse",
                schema: "dbo",
                newName: "MapReverses");

            migrationBuilder.AlterColumn<string>(
                name: "Village",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RuralDistrict",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Primary",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Poi",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plaque",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Penult",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Neighbourhood",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "MapReverses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "MapReverses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)");

            migrationBuilder.AlterColumn<string>(
                name: "Last",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "County",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "MapReverses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapReverses",
                table: "MapReverses",
                column: "Id");

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
    }
}
