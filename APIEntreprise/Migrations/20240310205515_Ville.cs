using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcSchool.Migrations
{
    public partial class Ville : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalarieSites");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneFix",
                table: "Salaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VilleId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VilleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_VilleId",
                table: "Salaries",
                column: "VilleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Ville_VilleId",
                table: "Salaries",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Ville_VilleId",
                table: "Salaries");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_VilleId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "TelephoneFix",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "VilleId",
                table: "Salaries");

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalarieSites",
                columns: table => new
                {
                    SalariesId = table.Column<int>(type: "int", nullable: false),
                    SitesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarieSites", x => new { x.SalariesId, x.SitesId });
                    table.ForeignKey(
                        name: "FK_SalarieSites_Salaries_SalariesId",
                        column: x => x.SalariesId,
                        principalTable: "Salaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalarieSites_Sites_SitesId",
                        column: x => x.SitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalarieSites_SitesId",
                table: "SalarieSites",
                column: "SitesId");
        }
    }
}
