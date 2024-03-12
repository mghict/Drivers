using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName_Password",
                table: "Users",
                columns: new[] { "UserName", "Password" });

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonCode",
                table: "Persons",
                column: "PersonCode");

            migrationBuilder.CreateIndex(
                name: "IX_Permissio_PermisionId",
                table: "Permissions",
                column: "PermisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_Guid",
                table: "Documents",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Auto_DeviceCode",
                table: "AutoDefinations",
                column: "DeviceCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_UserName_Password",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Person_PersonCode",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Permissio_PermisionId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Document_Guid",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Auto_DeviceCode",
                table: "AutoDefinations");
        }
    }
}
