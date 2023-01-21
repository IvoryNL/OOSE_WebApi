using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RestoredManyToManyGebruikerKlas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Klassen_Gebruikers");

            migrationBuilder.CreateTable(
                name: "Klas_Gebruiker",
                columns: table => new
                {
                    GebruikersId = table.Column<int>(type: "int", nullable: false),
                    KlassenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klas_Gebruiker", x => new { x.GebruikersId, x.KlassenId });
                    table.ForeignKey(
                        name: "FK_Klas_Gebruiker_Gebruikers_GebruikersId",
                        column: x => x.GebruikersId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Klas_Gebruiker_Klassen_KlassenId",
                        column: x => x.KlassenId,
                        principalTable: "Klassen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klas_Gebruiker_KlassenId",
                table: "Klas_Gebruiker",
                column: "KlassenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Klas_Gebruiker");

            migrationBuilder.CreateTable(
                name: "Klassen_Gebruikers",
                columns: table => new
                {
                    GebruikerId = table.Column<int>(type: "int", nullable: false),
                    KlasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klassen_Gebruikers", x => new { x.GebruikerId, x.KlasId });
                    table.ForeignKey(
                        name: "FK_Klassen_Gebruikers_Gebruikers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Klassen_Gebruikers_Klassen_KlasId",
                        column: x => x.KlasId,
                        principalTable: "Klassen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klassen_Gebruikers_KlasId",
                table: "Klassen_Gebruikers",
                column: "KlasId");
        }
    }
}
