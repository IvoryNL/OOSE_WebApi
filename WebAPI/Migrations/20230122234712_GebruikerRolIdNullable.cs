using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class GebruikerRolIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruikers_Rollen_RolId",
                table: "Gebruikers");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Gebruikers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruikers_Rollen_RolId",
                table: "Gebruikers",
                column: "RolId",
                principalTable: "Rollen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gebruikers_Rollen_RolId",
                table: "Gebruikers");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Gebruikers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gebruikers_Rollen_RolId",
                table: "Gebruikers",
                column: "RolId",
                principalTable: "Rollen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
