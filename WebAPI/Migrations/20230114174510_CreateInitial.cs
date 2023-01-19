using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klassen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Klasnaam = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klassen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LesmateriaalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LesmateriaalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Omschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Onderwijseenheden",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Coordinator = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Studiepunten = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderwijseenheden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rollen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollen", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Vormen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vormen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lesmaterialen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuteurId = table.Column<int>(type: "int", nullable: false),
                    LesmateriaaltypeId = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Verplicht = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesmaterialen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesmaterialen_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesmaterialen_LesmateriaalTypes_LesmateriaaltypeId",
                        column: x => x.LesmateriaaltypeId,
                        principalTable: "LesmateriaalTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leerdoelen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnderwijseenheidId = table.Column<int>(type: "int", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leerdoelen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leerdoelen_Onderwijseenheden_OnderwijseenheidId",
                        column: x => x.OnderwijseenheidId,
                        principalTable: "Onderwijseenheden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opleidingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VormId = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opleidingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opleidingen_Vormen_VormId",
                        column: x => x.VormId,
                        principalTable: "Vormen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tentamens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VormId = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnderwijseenheidId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tentamens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tentamens_Onderwijseenheden_OnderwijseenheidId",
                        column: x => x.OnderwijseenheidId,
                        principalTable: "Onderwijseenheden",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tentamens_Vormen_VormId",
                        column: x => x.VormId,
                        principalTable: "Vormen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Les_Lesmateriaal",
                columns: table => new
                {
                    LesmaterialenId = table.Column<int>(type: "int", nullable: false),
                    LessenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Les_Lesmateriaal", x => new { x.LesmaterialenId, x.LessenId });
                    table.ForeignKey(
                        name: "FK_Les_Lesmateriaal_Lesmaterialen_LesmaterialenId",
                        column: x => x.LesmaterialenId,
                        principalTable: "Lesmaterialen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Les_Lesmateriaal_Lessen_LessenId",
                        column: x => x.LessenId,
                        principalTable: "Lessen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leeruitkomsten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeerdoelId = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leeruitkomsten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leeruitkomsten_Leerdoelen_LeerdoelId",
                        column: x => x.LeerdoelId,
                        principalTable: "Leerdoelen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onderwijsmodules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpleidingId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Coordinator = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Studiepunten = table.Column<int>(type: "int", nullable: false),
                    Fase = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ingangseisen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Leerjaar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderwijsmodules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderwijsmodules_Opleidingen_OpleidingId",
                        column: x => x.OpleidingId,
                        principalTable: "Opleidingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onderwijsmodules_Statussen_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statussen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Opleidingsprofielen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpleidingId = table.Column<int>(type: "int", nullable: false),
                    Profielnaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opleidingsprofielen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opleidingsprofielen_Opleidingen_OpleidingId",
                        column: x => x.OpleidingId,
                        principalTable: "Opleidingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leeruitkomst_Les",
                columns: table => new
                {
                    LeeruitkomstenId = table.Column<int>(type: "int", nullable: false),
                    LessenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leeruitkomst_Les", x => new { x.LeeruitkomstenId, x.LessenId });
                    table.ForeignKey(
                        name: "FK_Leeruitkomst_Les_Leeruitkomsten_LeeruitkomstenId",
                        column: x => x.LeeruitkomstenId,
                        principalTable: "Leeruitkomsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leeruitkomst_Les_Lessen_LessenId",
                        column: x => x.LessenId,
                        principalTable: "Lessen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leeruitkomst_Tentamen",
                columns: table => new
                {
                    LeeruitkomstenId = table.Column<int>(type: "int", nullable: false),
                    TentamensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leeruitkomst_Tentamen", x => new { x.LeeruitkomstenId, x.TentamensId });
                    table.ForeignKey(
                        name: "FK_Leeruitkomst_Tentamen_Leeruitkomsten_LeeruitkomstenId",
                        column: x => x.LeeruitkomstenId,
                        principalTable: "Leeruitkomsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leeruitkomst_Tentamen_Tentamens_TentamensId",
                        column: x => x.TentamensId,
                        principalTable: "Tentamens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onderwijseenheid_Onderwijsmodule",
                columns: table => new
                {
                    OnderwijseenhedenId = table.Column<int>(type: "int", nullable: false),
                    OnderwijsmodulesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderwijseenheid_Onderwijsmodule", x => new { x.OnderwijseenhedenId, x.OnderwijsmodulesId });
                    table.ForeignKey(
                        name: "FK_Onderwijseenheid_Onderwijsmodule_Onderwijseenheden_OnderwijseenhedenId",
                        column: x => x.OnderwijseenhedenId,
                        principalTable: "Onderwijseenheden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onderwijseenheid_Onderwijsmodule_Onderwijsmodules_OnderwijsmodulesId",
                        column: x => x.OnderwijsmodulesId,
                        principalTable: "Onderwijsmodules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    OpleidingId = table.Column<int>(type: "int", nullable: true),
                    OpleidingsprofielId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gebruikers_Opleidingen_OpleidingId",
                        column: x => x.OpleidingId,
                        principalTable: "Opleidingen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gebruikers_Opleidingsprofielen_OpleidingsprofielId",
                        column: x => x.OpleidingsprofielId,
                        principalTable: "Opleidingsprofielen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gebruikers_Rollen_RolId",
                        column: x => x.RolId,
                        principalTable: "Rollen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beoordelingsmodellen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TentamenId = table.Column<int>(type: "int", nullable: false),
                    DocentId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beoordelingsmodellen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beoordelingsmodellen_Gebruikers_DocentId",
                        column: x => x.DocentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beoordelingsmodellen_Statussen_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statussen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beoordelingsmodellen_Tentamens_TentamenId",
                        column: x => x.TentamenId,
                        principalTable: "Tentamens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker_Onderwijsmodule",
                columns: table => new
                {
                    DocentenId = table.Column<int>(type: "int", nullable: false),
                    OnderwijsmodulesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker_Onderwijsmodule", x => new { x.DocentenId, x.OnderwijsmodulesId });
                    table.ForeignKey(
                        name: "FK_Gebruiker_Onderwijsmodule_Gebruikers_DocentenId",
                        column: x => x.DocentenId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gebruiker_Onderwijsmodule_Onderwijsmodules_OnderwijsmodulesId",
                        column: x => x.OnderwijsmodulesId,
                        principalTable: "Onderwijsmodules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klas_Gebruiker",
                columns: table => new
                {
                    GebruikersId = table.Column<int>(type: "int", nullable: false),
                    KlassenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klas_Gebruiker", x => new { x.GebruikersId, x.KlassenId });
                    table.ForeignKey(
                        name: "FK_Klas_Gebruiker_Gebruikers_GebruikersId",
                        column: x => x.GebruikersId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Klas_Gebruiker_Klassen_KlassenId",
                        column: x => x.KlassenId,
                        principalTable: "Klassen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onderwijsuitvoeringen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnderwijsmoduleId = table.Column<int>(type: "int", nullable: false),
                    DocentId = table.Column<int>(type: "int", nullable: false),
                    Jaartal = table.Column<int>(type: "int", nullable: false),
                    Periode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderwijsuitvoeringen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderwijsuitvoeringen_Gebruikers_DocentId",
                        column: x => x.DocentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Onderwijsuitvoeringen_Onderwijsmodules_OnderwijsmoduleId",
                        column: x => x.OnderwijsmoduleId,
                        principalTable: "Onderwijsmodules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beoordelingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DocentId = table.Column<int>(type: "int", nullable: false),
                    BeoordelingsmodelId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultaat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beoordelingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beoordelingen_Beoordelingsmodellen_BeoordelingsmodelId",
                        column: x => x.BeoordelingsmodelId,
                        principalTable: "Beoordelingsmodellen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beoordelingen_Gebruikers_DocentId",
                        column: x => x.DocentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beoordelingen_Gebruikers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beoordelingen_Statussen_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statussen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Beoordelingsonderdelen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeoordelingsmodelId = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Resultaat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gewicht = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Grens = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Verplicht = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beoordelingsonderdelen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beoordelingsonderdelen_Beoordelingsmodellen_BeoordelingsmodelId",
                        column: x => x.BeoordelingsmodelId,
                        principalTable: "Beoordelingsmodellen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klas_Onderwijsuitvoering",
                columns: table => new
                {
                    KlassenId = table.Column<int>(type: "int", nullable: false),
                    OnderwijsuitvoeringenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klas_Onderwijsuitvoering", x => new { x.KlassenId, x.OnderwijsuitvoeringenId });
                    table.ForeignKey(
                        name: "FK_Klas_Onderwijsuitvoering_Klassen_KlassenId",
                        column: x => x.KlassenId,
                        principalTable: "Klassen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Klas_Onderwijsuitvoering_Onderwijsuitvoeringen_OnderwijsuitvoeringenId",
                        column: x => x.OnderwijsuitvoeringenId,
                        principalTable: "Onderwijsuitvoeringen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planningen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnderwijsuitvoeringId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weeknummer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planningen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planningen_Onderwijsuitvoeringen_OnderwijsuitvoeringId",
                        column: x => x.OnderwijsuitvoeringId,
                        principalTable: "Onderwijsuitvoeringen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beoordelingscriterium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeoordelingsonderdeelId = table.Column<int>(type: "int", nullable: false),
                    LeeruitkomstId = table.Column<int>(type: "int", nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Resultaat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gewicht = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Grens = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Verplicht = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beoordelingscriterium", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beoordelingscriterium_Beoordelingsonderdelen_BeoordelingsonderdeelId",
                        column: x => x.BeoordelingsonderdeelId,
                        principalTable: "Beoordelingsonderdelen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beoordelingscriterium_Leeruitkomsten_LeeruitkomstId",
                        column: x => x.LeeruitkomstId,
                        principalTable: "Leeruitkomsten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Les_Planning",
                columns: table => new
                {
                    LessenId = table.Column<int>(type: "int", nullable: false),
                    PlanningenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Les_Planning", x => new { x.LessenId, x.PlanningenId });
                    table.ForeignKey(
                        name: "FK_Les_Planning_Lessen_LessenId",
                        column: x => x.LessenId,
                        principalTable: "Lessen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Les_Planning_Planningen_PlanningenId",
                        column: x => x.PlanningenId,
                        principalTable: "Planningen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tentamen_Planning",
                columns: table => new
                {
                    PlanningenId = table.Column<int>(type: "int", nullable: false),
                    TentamensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tentamen_Planning", x => new { x.PlanningenId, x.TentamensId });
                    table.ForeignKey(
                        name: "FK_Tentamen_Planning_Planningen_PlanningenId",
                        column: x => x.PlanningenId,
                        principalTable: "Planningen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tentamen_Planning_Tentamens_TentamensId",
                        column: x => x.TentamensId,
                        principalTable: "Tentamens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Toetsinschrijvingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TentamenId = table.Column<int>(type: "int", nullable: false),
                    PlanningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toetsinschrijvingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Toetsinschrijvingen_Gebruikers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toetsinschrijvingen_Planningen_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planningen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toetsinschrijvingen_Tentamens_TentamenId",
                        column: x => x.TentamenId,
                        principalTable: "Tentamens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beoordelingsdimensies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeoordelingscriteriaId = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Toelichting = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cijferwaarde = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beoordelingsdimensies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beoordelingsdimensies_Beoordelingscriterium_BeoordelingscriteriaId",
                        column: x => x.BeoordelingscriteriaId,
                        principalTable: "Beoordelingscriterium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auteurs_Naam",
                table: "Auteurs",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_BeoordelingsmodelId",
                table: "Beoordelingen",
                column: "BeoordelingsmodelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_DocentId",
                table: "Beoordelingen",
                column: "DocentId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_StatusId",
                table: "Beoordelingen",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingen_StudentId",
                table: "Beoordelingen",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingscriterium_BeoordelingsonderdeelId",
                table: "Beoordelingscriterium",
                column: "BeoordelingsonderdeelId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingscriterium_Criteria",
                table: "Beoordelingscriterium",
                column: "Criteria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingscriterium_LeeruitkomstId",
                table: "Beoordelingscriterium",
                column: "LeeruitkomstId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsdimensies_BeoordelingscriteriaId",
                table: "Beoordelingsdimensies",
                column: "BeoordelingscriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsdimensies_Titel",
                table: "Beoordelingsdimensies",
                column: "Titel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsmodellen_DocentId",
                table: "Beoordelingsmodellen",
                column: "DocentId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsmodellen_StatusId",
                table: "Beoordelingsmodellen",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsmodellen_TentamenId",
                table: "Beoordelingsmodellen",
                column: "TentamenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsonderdelen_BeoordelingsmodelId",
                table: "Beoordelingsonderdelen",
                column: "BeoordelingsmodelId");

            migrationBuilder.CreateIndex(
                name: "IX_Beoordelingsonderdelen_Titel",
                table: "Beoordelingsonderdelen",
                column: "Titel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_Onderwijsmodule_OnderwijsmodulesId",
                table: "Gebruiker_Onderwijsmodule",
                column: "OnderwijsmodulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_Email",
                table: "Gebruikers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_OpleidingId",
                table: "Gebruikers",
                column: "OpleidingId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_OpleidingsprofielId",
                table: "Gebruikers",
                column: "OpleidingsprofielId");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_RolId",
                table: "Gebruikers",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Klas_Gebruiker_KlassenId",
                table: "Klas_Gebruiker",
                column: "KlassenId");

            migrationBuilder.CreateIndex(
                name: "IX_Klas_Onderwijsuitvoering_OnderwijsuitvoeringenId",
                table: "Klas_Onderwijsuitvoering",
                column: "OnderwijsuitvoeringenId");

            migrationBuilder.CreateIndex(
                name: "IX_Klassen_Klasnaam",
                table: "Klassen",
                column: "Klasnaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leerdoelen_OnderwijseenheidId",
                table: "Leerdoelen",
                column: "OnderwijseenheidId");

            migrationBuilder.CreateIndex(
                name: "IX_Leeruitkomst_Les_LessenId",
                table: "Leeruitkomst_Les",
                column: "LessenId");

            migrationBuilder.CreateIndex(
                name: "IX_Leeruitkomst_Tentamen_TentamensId",
                table: "Leeruitkomst_Tentamen",
                column: "TentamensId");

            migrationBuilder.CreateIndex(
                name: "IX_Leeruitkomsten_LeerdoelId",
                table: "Leeruitkomsten",
                column: "LeerdoelId");

            migrationBuilder.CreateIndex(
                name: "IX_Leeruitkomsten_Naam",
                table: "Leeruitkomsten",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Les_Lesmateriaal_LessenId",
                table: "Les_Lesmateriaal",
                column: "LessenId");

            migrationBuilder.CreateIndex(
                name: "IX_Les_Planning_PlanningenId",
                table: "Les_Planning",
                column: "PlanningenId");

            migrationBuilder.CreateIndex(
                name: "IX_LesmateriaalTypes_Naam",
                table: "LesmateriaalTypes",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesmaterialen_AuteurId",
                table: "Lesmaterialen",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesmaterialen_LesmateriaaltypeId",
                table: "Lesmaterialen",
                column: "LesmateriaaltypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesmaterialen_Naam",
                table: "Lesmaterialen",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijseenheden_Naam",
                table: "Onderwijseenheden",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijseenheid_Onderwijsmodule_OnderwijsmodulesId",
                table: "Onderwijseenheid_Onderwijsmodule",
                column: "OnderwijsmodulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsmodules_Naam",
                table: "Onderwijsmodules",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsmodules_OpleidingId",
                table: "Onderwijsmodules",
                column: "OpleidingId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsmodules_StatusId",
                table: "Onderwijsmodules",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsuitvoeringen_DocentId",
                table: "Onderwijsuitvoeringen",
                column: "DocentId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsuitvoeringen_Jaartal_Periode",
                table: "Onderwijsuitvoeringen",
                columns: new[] { "Jaartal", "Periode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Onderwijsuitvoeringen_OnderwijsmoduleId",
                table: "Onderwijsuitvoeringen",
                column: "OnderwijsmoduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Opleidingen_Naam",
                table: "Opleidingen",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opleidingen_VormId",
                table: "Opleidingen",
                column: "VormId");

            migrationBuilder.CreateIndex(
                name: "IX_Opleidingsprofielen_OpleidingId",
                table: "Opleidingsprofielen",
                column: "OpleidingId");

            migrationBuilder.CreateIndex(
                name: "IX_Opleidingsprofielen_Profielnaam",
                table: "Opleidingsprofielen",
                column: "Profielnaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planningen_Datum_Weeknummer",
                table: "Planningen",
                columns: new[] { "Datum", "Weeknummer" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planningen_OnderwijsuitvoeringId",
                table: "Planningen",
                column: "OnderwijsuitvoeringId");

            migrationBuilder.CreateIndex(
                name: "IX_Rollen_Naam",
                table: "Rollen",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statussen_Naam",
                table: "Statussen",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tentamen_Planning_TentamensId",
                table: "Tentamen_Planning",
                column: "TentamensId");

            migrationBuilder.CreateIndex(
                name: "IX_Tentamens_Naam",
                table: "Tentamens",
                column: "Naam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tentamens_OnderwijseenheidId",
                table: "Tentamens",
                column: "OnderwijseenheidId");

            migrationBuilder.CreateIndex(
                name: "IX_Tentamens_VormId",
                table: "Tentamens",
                column: "VormId");

            migrationBuilder.CreateIndex(
                name: "IX_Toetsinschrijvingen_PlanningId",
                table: "Toetsinschrijvingen",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Toetsinschrijvingen_StudentId_TentamenId_PlanningId",
                table: "Toetsinschrijvingen",
                columns: new[] { "StudentId", "TentamenId", "PlanningId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toetsinschrijvingen_TentamenId",
                table: "Toetsinschrijvingen",
                column: "TentamenId");

            migrationBuilder.CreateIndex(
                name: "IX_Vormen_Naam",
                table: "Vormen",
                column: "Naam",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beoordelingen");

            migrationBuilder.DropTable(
                name: "Beoordelingsdimensies");

            migrationBuilder.DropTable(
                name: "Gebruiker_Onderwijsmodule");

            migrationBuilder.DropTable(
                name: "Klas_Gebruiker");

            migrationBuilder.DropTable(
                name: "Klas_Onderwijsuitvoering");

            migrationBuilder.DropTable(
                name: "Leeruitkomst_Les");

            migrationBuilder.DropTable(
                name: "Leeruitkomst_Tentamen");

            migrationBuilder.DropTable(
                name: "Les_Lesmateriaal");

            migrationBuilder.DropTable(
                name: "Les_Planning");

            migrationBuilder.DropTable(
                name: "Onderwijseenheid_Onderwijsmodule");

            migrationBuilder.DropTable(
                name: "Tentamen_Planning");

            migrationBuilder.DropTable(
                name: "Toetsinschrijvingen");

            migrationBuilder.DropTable(
                name: "Beoordelingscriterium");

            migrationBuilder.DropTable(
                name: "Klassen");

            migrationBuilder.DropTable(
                name: "Lesmaterialen");

            migrationBuilder.DropTable(
                name: "Lessen");

            migrationBuilder.DropTable(
                name: "Planningen");

            migrationBuilder.DropTable(
                name: "Beoordelingsonderdelen");

            migrationBuilder.DropTable(
                name: "Leeruitkomsten");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropTable(
                name: "LesmateriaalTypes");

            migrationBuilder.DropTable(
                name: "Onderwijsuitvoeringen");

            migrationBuilder.DropTable(
                name: "Beoordelingsmodellen");

            migrationBuilder.DropTable(
                name: "Leerdoelen");

            migrationBuilder.DropTable(
                name: "Onderwijsmodules");

            migrationBuilder.DropTable(
                name: "Gebruikers");

            migrationBuilder.DropTable(
                name: "Tentamens");

            migrationBuilder.DropTable(
                name: "Statussen");

            migrationBuilder.DropTable(
                name: "Opleidingsprofielen");

            migrationBuilder.DropTable(
                name: "Rollen");

            migrationBuilder.DropTable(
                name: "Onderwijseenheden");

            migrationBuilder.DropTable(
                name: "Opleidingen");

            migrationBuilder.DropTable(
                name: "Vormen");
        }
    }
}
