using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Gebruikers_GebruikersId",
                table: "Klas_Gebruiker");

            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Klassen_KlassenId",
                table: "Klas_Gebruiker");

            migrationBuilder.RenameColumn(
                name: "KlassenId",
                table: "Klas_Gebruiker",
                newName: "KlasId");

            migrationBuilder.RenameColumn(
                name: "GebruikersId",
                table: "Klas_Gebruiker",
                newName: "GebruikerId");

            migrationBuilder.RenameIndex(
                name: "IX_Klas_Gebruiker_KlassenId",
                table: "Klas_Gebruiker",
                newName: "IX_Klas_Gebruiker_KlasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klas_Gebruiker_Gebruikers_GebruikerId",
                table: "Klas_Gebruiker",
                column: "GebruikerId",
                principalTable: "Gebruikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klas_Gebruiker_Klassen_KlasId",
                table: "Klas_Gebruiker",
                column: "KlasId",
                principalTable: "Klassen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Gebruikers_GebruikerId",
                table: "Klas_Gebruiker");

            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Klassen_KlasId",
                table: "Klas_Gebruiker");

            migrationBuilder.RenameColumn(
                name: "KlasId",
                table: "Klas_Gebruiker",
                newName: "KlassenId");

            migrationBuilder.RenameColumn(
                name: "GebruikerId",
                table: "Klas_Gebruiker",
                newName: "GebruikersId");

            migrationBuilder.RenameIndex(
                name: "IX_Klas_Gebruiker_KlasId",
                table: "Klas_Gebruiker",
                newName: "IX_Klas_Gebruiker_KlassenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klas_Gebruiker_Gebruikers_GebruikersId",
                table: "Klas_Gebruiker",
                column: "GebruikersId",
                principalTable: "Gebruikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klas_Gebruiker_Klassen_KlassenId",
                table: "Klas_Gebruiker",
                column: "KlassenId",
                principalTable: "Klassen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
