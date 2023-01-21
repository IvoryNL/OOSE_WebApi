using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLesmateriaalVorm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LesmateriaalVormen");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Gebruikers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Gebruikers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Gebruikers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Gebruikers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LesmateriaalVormen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LesmateriaalId = table.Column<int>(type: "int", nullable: false),
                    Bestandstype = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Structuur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Versie = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_LesmateriaalVormen_LesmateriaalId",
                table: "LesmateriaalVormen",
                column: "LesmateriaalId");
        }
    }
}
