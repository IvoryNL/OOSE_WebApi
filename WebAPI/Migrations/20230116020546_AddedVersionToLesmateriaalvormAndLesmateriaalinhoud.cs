using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedVersionToLesmateriaalvormAndLesmateriaalinhoud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Versie",
                table: "Lesmaterialen");

            migrationBuilder.AddColumn<string>(
                name: "Versie",
                table: "LesmateriaalVormen",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Versie",
                table: "LesmateriaalInhoud",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Versie",
                table: "LesmateriaalVormen");

            migrationBuilder.DropColumn(
                name: "Versie",
                table: "LesmateriaalInhoud");

            migrationBuilder.AddColumn<string>(
                name: "Versie",
                table: "Lesmaterialen",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
