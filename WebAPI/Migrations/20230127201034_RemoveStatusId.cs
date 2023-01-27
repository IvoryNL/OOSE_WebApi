using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Onderwijsmodules");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Beoordelingsmodellen");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Beoordelingen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Onderwijsmodules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Beoordelingsmodellen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Beoordelingen",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
