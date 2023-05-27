using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class eventotoevaluacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegioId",
                table: "EventoAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_Eventos_EvaluacionId",
                table: "EventoAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_MateriaListas_MateriaListaId",
                table: "Eventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventoAlumnos",
                table: "EventoAlumnos");

            migrationBuilder.RenameTable(
                name: "Eventos",
                newName: "Evaluaciones");

            migrationBuilder.RenameTable(
                name: "EventoAlumnos",
                newName: "EvaluacionAlumnos");

            migrationBuilder.RenameIndex(
                name: "IX_Eventos_MateriaListaId",
                table: "Evaluaciones",
                newName: "IX_Evaluaciones_MateriaListaId");

            migrationBuilder.RenameIndex(
                name: "IX_EventoAlumnos_EvaluacionId",
                table: "EvaluacionAlumnos",
                newName: "IX_EvaluacionAlumnos_EvaluacionId");

            migrationBuilder.RenameIndex(
                name: "IX_EventoAlumnos_ClasesAlumnosColegioId",
                table: "EvaluacionAlumnos",
                newName: "IX_EvaluacionAlumnos_ClasesAlumnosColegioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evaluaciones",
                table: "Evaluaciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluacionAlumnos",
                table: "EvaluacionAlumnos",
                column: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegi~",
                table: "EvaluacionAlumnos",
                column: "ClasesAlumnosColegioId",
                principalTable: "ClasesAlumnosColegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluacionAlumnos_Evaluaciones_EvaluacionId",
                table: "EvaluacionAlumnos",
                column: "EvaluacionId",
                principalTable: "Evaluaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluaciones_MateriaListas_MateriaListaId",
                table: "Evaluaciones",
                column: "MateriaListaId",
                principalTable: "MateriaListas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegi~",
                table: "EvaluacionAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluacionAlumnos_Evaluaciones_EvaluacionId",
                table: "EvaluacionAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluaciones_MateriaListas_MateriaListaId",
                table: "Evaluaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evaluaciones",
                table: "Evaluaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluacionAlumnos",
                table: "EvaluacionAlumnos");
            
            migrationBuilder.RenameTable(
                name: "Evaluaciones",
                newName: "Eventos");

            migrationBuilder.RenameTable(
                name: "EvaluacionAlumnos",
                newName: "EventoAlumnos");

            migrationBuilder.RenameIndex(
                name: "IX_Evaluaciones_MateriaListaId",
                table: "Eventos",
                newName: "IX_Eventos_MateriaListaId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionAlumnos_EvaluacionId",
                table: "EventoAlumnos",
                newName: "IX_EventoAlumnos_EvaluacionId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluacionAlumnos_ClasesAlumnosColegioId",
                table: "EventoAlumnos",
                newName: "IX_EventoAlumnos_ClasesAlumnosColegioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventoAlumnos",
                table: "EventoAlumnos",
                column: "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegioId",
                table: "EventoAlumnos",
                column: "ClasesAlumnosColegioId",
                principalTable: "ClasesAlumnosColegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_Eventos_EvaluacionId",
                table: "EventoAlumnos",
                column: "EvaluacionId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_MateriaListas_MateriaListaId",
                table: "Eventos",
                column: "MateriaListaId",
                principalTable: "MateriaListas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
