using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class calif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_AspNetUsers_ProfesoresId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Clases_ClaseId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Colegios_ColegioId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Materias_MateriaId",
                table: "Eventos");

            migrationBuilder.DropTable(
                name: "Rubricas");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_ClaseId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_ColegioId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_ProfesoresId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ClaseId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ColegioId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "ProfesoresId",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "Eventos",
                newName: "MateriaListaId");

            migrationBuilder.RenameIndex(
                name: "IX_Eventos_MateriaId",
                table: "Eventos",
                newName: "IX_Eventos_MateriaListaId");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Eventos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_MateriaListas_MateriaListaId",
                table: "Eventos",
                column: "MateriaListaId",
                principalTable: "MateriaListas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_MateriaListas_MateriaListaId",
                table: "Eventos");
            migrationBuilder.RenameColumn(
                name: "MateriaListaId",
                table: "Eventos",
                newName: "MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Eventos_MateriaListaId",
                table: "Eventos",
                newName: "IX_Eventos_MateriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Eventos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClaseId",
                table: "Eventos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColegioId",
                table: "Eventos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfesorId",
                table: "Eventos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfesoresId",
                table: "Eventos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rubricas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    NombreRubrica = table.Column<string>(type: "text", nullable: false),
                    Puntaje = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubricas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rubricas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ClaseId",
                table: "Eventos",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ColegioId",
                table: "Eventos",
                column: "ColegioId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ProfesoresId",
                table: "Eventos",
                column: "ProfesoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubricas_EventoId",
                table: "Rubricas",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_AspNetUsers_ProfesoresId",
                table: "Eventos",
                column: "ProfesoresId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Clases_ClaseId",
                table: "Eventos",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Colegios_ColegioId",
                table: "Eventos",
                column: "ColegioId",
                principalTable: "Colegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Materias_MateriaId",
                table: "Eventos",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
