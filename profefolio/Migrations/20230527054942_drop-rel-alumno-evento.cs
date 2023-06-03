using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class droprelalumnoevento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_AspNetUsers_AlumnoId",
                table: "EventoAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_Eventos_EventoId",
                table: "EventoAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_EventoAlumnos_AlumnoId",
                table: "EventoAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_EventoAlumnos_EventoId",
                table: "EventoAlumnos");
            

            migrationBuilder.DropColumn(
                name: "AlumnoId",
                table: "EventoAlumnos");

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "EventoAlumnos");

            migrationBuilder.RenameColumn(
                name: "IdCalificacion",
                table: "EventoAlumnos",
                newName: "EvaluacionId");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Eventos",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Etapa",
                table: "Eventos",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EventoAlumnos_EvaluacionId",
                table: "EventoAlumnos",
                column: "EvaluacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_Eventos_EvaluacionId",
                table: "EventoAlumnos",
                column: "EvaluacionId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_Eventos_EvaluacionId",
                table: "EventoAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_EventoAlumnos_EvaluacionId",
                table: "EventoAlumnos");
            

            migrationBuilder.DropColumn(
                name: "Etapa",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "EvaluacionId",
                table: "EventoAlumnos",
                newName: "IdCalificacion");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Eventos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "AlumnoId",
                table: "EventoAlumnos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventoId",
                table: "EventoAlumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventoAlumnos_AlumnoId",
                table: "EventoAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoAlumnos_EventoId",
                table: "EventoAlumnos",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_AspNetUsers_AlumnoId",
                table: "EventoAlumnos",
                column: "AlumnoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_Eventos_EventoId",
                table: "EventoAlumnos",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
