using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class claseAlumnoColegio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

         
            migrationBuilder.CreateTable(
                name: "ClasesAlumnosColegios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClaseId = table.Column<int>(type: "integer", nullable: false),
                    ColegiosAlumnosId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasesAlumnosColegios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClasesAlumnosColegios_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasesAlumnosColegios_ColegiosAlumnos_ColegiosAlumnosId",
                        column: x => x.ColegiosAlumnosId,
                        principalTable: "ColegiosAlumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClasesAlumnosColegios_ClaseId",
                table: "ClasesAlumnosColegios",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasesAlumnosColegios_ColegiosAlumnosId",
                table: "ClasesAlumnosColegios",
                column: "ColegiosAlumnosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasesAlumnosColegios");

            migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId");
        }
    }
}
