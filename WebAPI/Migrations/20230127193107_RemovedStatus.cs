using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_Statussen_StatusId",
                table: "Beoordelingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingsmodellen_Statussen_StatusId",
                table: "Beoordelingsmodellen");

            migrationBuilder.DropForeignKey(
                name: "FK_Onderwijsmodules_Statussen_StatusId",
                table: "Onderwijsmodules");

            migrationBuilder.DropTable(
                name: "Statussen");

            migrationBuilder.DropIndex(
                name: "IX_Onderwijsmodules_StatusId",
                table: "Onderwijsmodules");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingsmodellen_StatusId",
                table: "Beoordelingsmodellen");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingen_StatusId",
                table: "Beoordelingen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statussen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statussen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsmodules_StatusId",
                table: "Onderwijsmodules",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsmodellen_StatusId",
                table: "Beoordelingsmodellen",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_StatusId",
                table: "Beoordelingen",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Statussen_Naam",
                table: "Statussen",
                column: "Naam",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_Statussen_StatusId",
                table: "Beoordelingen",
                column: "StatusId",
                principalTable: "Statussen",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingsmodellen_Statussen_StatusId",
                table: "Beoordelingsmodellen",
                column: "StatusId",
                principalTable: "Statussen",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Onderwijsmodules_Statussen_StatusId",
                table: "Onderwijsmodules",
                column: "StatusId",
                principalTable: "Statussen",
                principalColumn: "Id");
        }
    }
}
