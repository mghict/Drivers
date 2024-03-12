using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_AutoId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PersonId",
                table: "Documents");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Persons",
                type: "bigint",
                nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "AutoId1",
            //    table: "Documents",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "PersonId1",
            //    table: "Documents",
            //    type: "bigint",
            //    nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "AutoDefinations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AutoId",
                table: "Documents",
                column: "AutoId",
                unique: true,
                filter: "[AutoId] IS NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Documents_AutoId1",
            //    table: "Documents",
            //    column: "AutoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId",
                table: "Documents",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Documents_PersonId1",
            //    table: "Documents",
            //    column: "PersonId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Documents_AutoDefinations_AutoId",
            //    table: "Documents",
            //    column: "AutoId",
            //    principalTable: "AutoDefinations",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Documents_Persons_PersonId",
            //    table: "Documents",
            //    column: "PersonId",
            //    principalTable: "Persons",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AutoDefinations_AutoId1",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Persons_PersonId1",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AutoId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AutoId1",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PersonId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PersonId1",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "AutoId1",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AutoDefinations");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AutoId",
                table: "Documents",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId",
                table: "Documents",
                column: "PersonId");
        }
    }
}
