using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment_back_end.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikelen",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotMaat = table.Column<int>(type: "int", nullable: false),
                    PlantHoogte = table.Column<int>(type: "int", nullable: false),
                    Kleur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductGroep = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikelen", x => x.Code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artikelen");
        }
    }
}
