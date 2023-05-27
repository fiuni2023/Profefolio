using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class addrelclasesalumnoscolegio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClasesAlumnosColegioId",
                table: "EventoAlumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.CreateIndex(
                name: "IX_EventoAlumnos_ClasesAlumnosColegioId",
                table: "EventoAlumnos",
                column: "ClasesAlumnosColegioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegioId",
                table: "EventoAlumnos",
                column: "ClasesAlumnosColegioId",
                principalTable: "ClasesAlumnosColegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_ClasesAlumnosColegios_ClasesAlumnosColegioId",
                table: "EventoAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_EventoAlumnos_ClasesAlumnosColegioId",
                table: "EventoAlumnos");
            
            migrationBuilder.DropColumn(
                name: "ClasesAlumnosColegioId",
                table: "EventoAlumnos");
            
        }
    }
}
