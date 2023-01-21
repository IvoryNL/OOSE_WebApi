using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Gebruikers_GebruikerId",
                table: "Klas_Gebruiker");

            migrationBuilder.DropForeignKey(
                name: "FK_Klas_Gebruiker_Klassen_KlasId",
                table: "Klas_Gebruiker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klas_Gebruiker",
                table: "Klas_Gebruiker");

            migrationBuilder.RenameTable(
                name: "Klas_Gebruiker",
                newName: "Klassen_Gebruikers");

            migrationBuilder.RenameIndex(
                name: "IX_Klas_Gebruiker_KlasId",
                table: "Klassen_Gebruikers",
                newName: "IX_Klassen_Gebruikers_KlasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klassen_Gebruikers",
                table: "Klassen_Gebruikers",
                columns: new[] { "GebruikerId", "KlasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Klassen_Gebruikers_Gebruikers_GebruikerId",
                table: "Klassen_Gebruikers",
                column: "GebruikerId",
                principalTable: "Gebruikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klassen_Gebruikers_Klassen_KlasId",
                table: "Klassen_Gebruikers",
                column: "KlasId",
                principalTable: "Klassen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klassen_Gebruikers_Gebruikers_GebruikerId",
                table: "Klassen_Gebruikers");

            migrationBuilder.DropForeignKey(
                name: "FK_Klassen_Gebruikers_Klassen_KlasId",
                table: "Klassen_Gebruikers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klassen_Gebruikers",
                table: "Klassen_Gebruikers");

            migrationBuilder.RenameTable(
                name: "Klassen_Gebruikers",
                newName: "Klas_Gebruiker");

            migrationBuilder.RenameIndex(
                name: "IX_Klassen_Gebruikers_KlasId",
                table: "Klas_Gebruiker",
                newName: "IX_Klas_Gebruiker_KlasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klas_Gebruiker",
                table: "Klas_Gebruiker",
                columns: new[] { "GebruikerId", "KlasId" });

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
    }
}
