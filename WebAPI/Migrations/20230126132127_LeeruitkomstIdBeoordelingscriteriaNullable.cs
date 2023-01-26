using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class LeeruitkomstIdBeoordelingscriteriaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingscriterium_Leeruitkomsten_LeeruitkomstId",
                table: "Beoordelingscriterium");

            migrationBuilder.AlterColumn<int>(
                name: "LeeruitkomstId",
                table: "Beoordelingscriterium",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingscriterium_Leeruitkomsten_LeeruitkomstId",
                table: "Beoordelingscriterium",
                column: "LeeruitkomstId",
                principalTable: "Leeruitkomsten",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beoordelingscriterium_Leeruitkomsten_LeeruitkomstId",
                table: "Beoordelingscriterium");

            migrationBuilder.AlterColumn<int>(
                name: "LeeruitkomstId",
                table: "Beoordelingscriterium",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beoordelingscriterium_Leeruitkomsten_LeeruitkomstId",
                table: "Beoordelingscriterium",
                column: "LeeruitkomstId",
                principalTable: "Leeruitkomsten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
