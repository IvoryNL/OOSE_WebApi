using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnBeoordelingen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingen_TentamenUploadId",
                table: "Beoordelingen");

            migrationBuilder.RenameColumn(
                name: "TentamenUploadId",
                table: "Beoordelingen",
                newName: "TentamenVanStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_TentamenVanStudentId",
                table: "Beoordelingen",
                column: "TentamenVanStudentId",
                unique: true,
                filter: "[TentamenVanStudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenVanStudentId",
                table: "Beoordelingen",
                column: "TentamenVanStudentId",
                principalTable: "TentamenVanStudenten",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenVanStudentId",
                table: "Beoordelingen");

            migrationBuilder.DropIndex(
                name: "IX_Beoordelingen_TentamenVanStudentId",
                table: "Beoordelingen");

            migrationBuilder.RenameColumn(
                name: "TentamenVanStudentId",
                table: "Beoordelingen",
                newName: "TentamenUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_TentamenUploadId",
                table: "Beoordelingen",
                column: "TentamenUploadId",
                unique: true,
                filter: "[TentamenUploadId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingen_TentamenVanStudenten_TentamenUploadId",
                table: "Beoordelingen",
                column: "TentamenUploadId",
                principalTable: "TentamenVanStudenten",
                principalColumn: "Id");
        }
    }
}
