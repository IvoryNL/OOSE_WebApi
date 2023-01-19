using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTentamUploadsToTentamenVanStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_TentamenUploads_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.DropTable(
                name: "TentamenUploads");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Beoordelingen");

            migrationBuilder.CreateTable(
                name: "TentamenVanStudenten",
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
                    table.PrimaryKey("PK_TentamenVanStudenten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TentamenVanStudenten_Gebruikers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TentamenVanStudenten_StudentId",
                table: "TentamenVanStudenten",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenUploadId",
                table: "Beoordelingen",
                column: "TentamenUploadId",
                principalTable: "TentamenVanStudenten",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.DropTable(
                name: "TentamenVanStudenten");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Beoordelingen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TentamenUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Bestand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TentamenId = table.Column<int>(type: "int", nullable: false)
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
    }
}
