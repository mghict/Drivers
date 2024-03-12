using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditAutoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AutoDefinations_AutoModelId",
                table: "AutoDefinations");

            //migrationBuilder.AddColumn<int>(
            //    name: "AutoBrandId1",
            //    table: "AutoModels",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "AutoDefinations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pelak",
                table: "AutoDefinations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AutoModels_AutoBrandId1",
            //    table: "AutoModels",
            //    column: "AutoBrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Auto_Pelak",
                table: "AutoDefinations",
                column: "Pelak");

            migrationBuilder.CreateIndex(
                name: "IX_Auto_VIN",
                table: "AutoDefinations",
                column: "VIN");

            migrationBuilder.CreateIndex(
                name: "IX_AutoDefinations_AutoModelId",
                table: "AutoDefinations",
                column: "AutoModelId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AutoModels_AutoBrands_AutoBrandId1",
            //    table: "AutoModels",
            //    column: "AutoBrandId1",
            //    principalTable: "AutoBrands",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoModels_AutoBrands_AutoBrandId1",
                table: "AutoModels");

            migrationBuilder.DropIndex(
                name: "IX_AutoModels_AutoBrandId1",
                table: "AutoModels");

            migrationBuilder.DropIndex(
                name: "IX_Auto_Pelak",
                table: "AutoDefinations");

            migrationBuilder.DropIndex(
                name: "IX_Auto_VIN",
                table: "AutoDefinations");

            migrationBuilder.DropIndex(
                name: "IX_AutoDefinations_AutoModelId",
                table: "AutoDefinations");

            migrationBuilder.DropColumn(
                name: "AutoBrandId1",
                table: "AutoModels");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "AutoDefinations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Pelak",
                table: "AutoDefinations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AutoDefinations_AutoModelId",
                table: "AutoDefinations",
                column: "AutoModelId",
                unique: true);
        }
    }
}
