using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImage01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AutoDefinations_AutoId",
                table: "Documents");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Documents_AutoDefinations_AutoId1",
            //    table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Persons_PersonId",
                table: "Documents");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Documents_Persons_PersonId1",
            //    table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AutoId",
                table: "Documents");

            //migrationBuilder.DropIndex(
            //    name: "IX_Documents_AutoId1",
            //    table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_PersonId",
                table: "Documents");

            //migrationBuilder.DropIndex(
            //    name: "IX_Documents_PersonId1",
            //    table: "Documents");

            //migrationBuilder.DropColumn(
            //    name: "AutoId1",
            //    table: "Documents");

            //migrationBuilder.DropColumn(
            //    name: "PersonId1",
            //    table: "Documents");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AutoDefinations");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Persons",
                newName: "DocumentId");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "AutoDefinations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DocumentId",
                table: "Persons",
                column: "DocumentId",
                unique: false,
                filter: "[DocumentId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AutoDefinations_DocumentId",
                table: "AutoDefinations",
                column: "DocumentId",
                unique: false,
                filter: "[DocumentId] IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoDefinations_Documents_DocumentId",
                table: "AutoDefinations",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Documents_DocumentId",
                table: "Persons",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoDefinations_Documents_DocumentId",
                table: "AutoDefinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Documents_DocumentId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DocumentId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_AutoDefinations_DocumentId",
                table: "AutoDefinations");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "AutoDefinations");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Persons",
                newName: "ImageId");

            migrationBuilder.AddColumn<long>(
                name: "AutoId1",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PersonId1",
                table: "Documents",
                type: "bigint",
                nullable: true);

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
                filter: "[AutoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AutoId1",
                table: "Documents",
                column: "AutoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId",
                table: "Documents",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId1",
                table: "Documents",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AutoDefinations_AutoId",
                table: "Documents",
                column: "AutoId",
                principalTable: "AutoDefinations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AutoDefinations_AutoId1",
                table: "Documents",
                column: "AutoId1",
                principalTable: "AutoDefinations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Persons_PersonId",
                table: "Documents",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Persons_PersonId1",
                table: "Documents",
                column: "PersonId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
