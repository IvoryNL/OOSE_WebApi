using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToBeoordelingsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Naam",
                table: "Beoordelingsmodellen",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsmodellen_Naam",
                table: "Beoordelingsmodellen",
                column: "Naam",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Beoordelingsmodellen_Naam",
                table: "Beoordelingsmodellen");

            migrationBuilder.DropColumn(
                name: "Naam",
                table: "Beoordelingsmodellen");
        }
    }
}
