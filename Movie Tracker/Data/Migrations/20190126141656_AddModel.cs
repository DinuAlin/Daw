using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Movie_Tracker.Data.Migrations
{
    public partial class AddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Regizor",
               columns: table => new
               {
                   Id = table.Column<Int64>(nullable: false)
                   .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                   Nume = table.Column<string>(maxLength: 256, nullable: true),
                   Prenume = table.Column<string>(maxLength: 256, nullable: true),
                   DataNastere = table.Column<DateTime>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Regizor", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "Scenarist",
                columns: table => new
                {
                    IdScenarist = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(maxLength: 256, nullable: true),
                    Prenume = table.Column<string>(maxLength: 256, nullable: true),
                    DataNastere = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarist", x => x.IdScenarist);
                });

            migrationBuilder.CreateTable(
                name: "Producator",
                columns: table => new
                {
                    IdProducator = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(maxLength: 256, nullable: true),
                    Prenume = table.Column<string>(maxLength: 256, nullable: true),
                    DataNastere = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producator", x => x.IdProducator);
                });

            migrationBuilder.CreateTable(
                name: "Compozitor",
                columns: table => new
                {
                    IdCompozitor = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(maxLength: 256, nullable: true),
                    Prenume = table.Column<string>(maxLength: 256, nullable: true),
                    DataNastere = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compozitor", x => x.IdCompozitor);
                });

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    IdActor = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(maxLength: 256, nullable: true),
                    Prenume = table.Column<string>(maxLength: 256, nullable: true),
                    DataNastere = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.IdActor);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    IdFilm = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(maxLength: 50, nullable: false),
                    Gen = table.Column<string>(maxLength: 256, nullable: true),
                    Durata = table.Column<string>(maxLength: 256, nullable: true),
                    DataLansare = table.Column<DateTime>(nullable: true),
                    IdRegizor = table.Column<Int64>(nullable: true),
                    IdScenarist = table.Column<Int64>(nullable: true),
                    IdRegizorNavigationId = table.Column<Int64>(nullable: true),
                    IdCompozitor = table.Column<Int64>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.IdFilm);
                    table.ForeignKey(
                        name: "FK_Film_Regizor_IdRegizorNavigationId",
                        column: x => x.IdRegizorNavigationId,
                        principalTable: "Regizor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Film_IdScenarist",
                        column: x => x.IdScenarist,
                        principalTable: "Scenarist",
                        principalColumn: "IdScenarist",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Film_IdCompozitor",
                        column: x => x.IdCompozitor,
                        principalTable: "Compozitor",
                        principalColumn: "IdCompozitor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmActor",
                columns: table => new
                {
                    IdFilmActor = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdActor = table.Column<Int64>(nullable: true),
                    IdFilm = table.Column<Int64>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmActor", x => x.IdFilmActor);
                    table.ForeignKey(
                       name: "FK_FilmActor_IdActor",
                       column: x => x.IdActor,
                       principalTable: "Actor",
                       principalColumn: "IdActor",
                       onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                       name: "FK_FilmActor_IdFilm",
                       column: x => x.IdFilm,
                       principalTable: "Film",
                       principalColumn: "IdFilm",
                       onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmProducator",
                columns: table => new
                {
                    IdFilmProducator = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProducator = table.Column<Int64>(nullable: true),
                    IdFilm = table.Column<Int64>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmProducator", x => x.IdFilmProducator);
                    table.ForeignKey(
                       name: "FK_FilmProducator_IdProducator",
                       column: x => x.IdProducator,
                       principalTable: "Producator",
                       principalColumn: "IdProducator",
                       onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                       name: "FK_FilmProducator_IdFilm",
                       column: x => x.IdFilm,
                       principalTable: "Film",
                       principalColumn: "IdFilm",
                       onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFilm",
                columns: table => new
                {
                    IdUserFilm = table.Column<Int64>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFilm = table.Column<Int64>(nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFilm", x => x.IdUserFilm);
                    table.ForeignKey(
                       name: "FK_UserFilm_IdFilm",
                       column: x => x.IdFilm,
                       principalTable: "Film",
                       principalColumn: "IdFilm",
                       onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFilm_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_IdCompozitor",
                table: "Film",
                column: "IdCompozitor");

            migrationBuilder.CreateIndex(
                name: "IX_Film_IdRegizor",
                table: "Film",
                column: "IdRegizor");

            migrationBuilder.CreateIndex(
                name: "IX_Film_IdScenarist",
                table: "Film",
                column: "IdScenarist");

            migrationBuilder.CreateIndex(
                name: "IX_FilmActor_IdActor",
                table: "FilmActor",
                column: "IdActor");

            migrationBuilder.CreateIndex(
                name: "IX_FilmActor_IdFilm",
                table: "FilmActor",
                column: "IdFilm");

            migrationBuilder.CreateIndex(
                name: "IX_FilmProducator_IdFilm",
                table: "FilmProducator",
                column: "IdFilm");

            migrationBuilder.CreateIndex(
                name: "IX_FilmProducator_IdProducator",
                table: "FilmProducator",
                column: "IdProducator");

            migrationBuilder.CreateIndex(
                name: "IX_UserFilm_IdFilm",
                table: "UserFilm",
                column: "IdFilm");

            migrationBuilder.CreateIndex(
            name: "IX_UserFilm_UserId",
            table: "UserFilm",
            column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Regizor");

            migrationBuilder.DropTable(
                name: "Scenarist");

            migrationBuilder.DropTable(
                name: "Producator");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Compozitor");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "FilmActor");

            migrationBuilder.DropTable(
                name: "FilmProducator");

            migrationBuilder.DropTable(
                name: "UserFilm");
        }
    }
}
