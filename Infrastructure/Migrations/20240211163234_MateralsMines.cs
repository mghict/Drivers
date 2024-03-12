using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MateralsMines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialMine",
                columns: table => new
                {
                    MaterialsId = table.Column<int>(type: "int", nullable: false),
                    MinesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMine", x => new { x.MaterialsId, x.MinesId });
                    table.ForeignKey(
                        name: "FK_MaterialMine_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialMine_MineDefinations_MinesId",
                        column: x => x.MinesId,
                        principalTable: "MineDefinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMine_MinesId",
                table: "MaterialMine",
                column: "MinesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialMine");
        }
    }
}
