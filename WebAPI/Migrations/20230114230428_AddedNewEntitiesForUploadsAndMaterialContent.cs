using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewEntitiesForUploadsAndMaterialContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_Gebruikers_StudentId",
                table: "Beoordelingen");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingen_StudentId",
                table: "Beoordelingen");

            migrationBuilder.AddColumn<int>(
                name: "TentamenUploadId",
                table: "Beoordelingen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LesmateriaalInhoud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesmateriaalId = table.Column<int>(type: "int", nullable: false),
                    Inhoud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Versie = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LesmateriaalInhoud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LesmateriaalInhoud_Lesmaterialen_LesmateriaalId",
                        column: x => x.LesmateriaalId,
                        principalTable: "Lesmaterialen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LesmateriaalVormen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesmateriaalId = table.Column<int>(type: "int", nullable: false),
                    Structuur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bestandstype = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LesmateriaalVormen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LesmateriaalVormen_Lesmaterialen_LesmateriaalId",
                        column: x => x.LesmateriaalId,
                        principalTable: "Lesmaterialen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TentamenUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TentamenId = table.Column<int>(type: "int", nullable: false),
                    Bestand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TentamenUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TentamenUploads_Gebruikers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_TentamenUploadId",
                table: "Beoordelingen",
                column: "TentamenUploadId",
                unique: true,
                filter: "[TentamenUploadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LesmateriaalInhoud_LesmateriaalId",
                table: "LesmateriaalInhoud",
                column: "LesmateriaalId");

            migrationBuilder.CreateIndex(
                name: "IX_LesmateriaalVormen_LesmateriaalId",
                table: "LesmateriaalVormen",
                column: "LesmateriaalId");

            migrationBuilder.CreateIndex(
                name: "IX_TentamenUploads_StudentId",
                table: "TentamenUploads",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_TentamenUploads_TentamenUploadId",
                table: "Beoordelingen",
                column: "TentamenUploadId",
                principalTable: "TentamenUploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_TentamenUploads_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.DropTable(
                name: "LesmateriaalInhoud");

            migrationBuilder.DropTable(
                name: "LesmateriaalVormen");

            migrationBuilder.DropTable(
                name: "TentamenUploads");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingen_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.DropColumn(
                name: "TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_StudentId",
                table: "Beoordelingen",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_Gebruikers_StudentId",
                table: "Beoordelingen",
                column: "StudentId",
                principalTable: "Gebruikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
