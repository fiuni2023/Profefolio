using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class nombre_en_ev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColegiosAlumnos_AspNetUsers_PersonaId",
                table: "ColegiosAlumnos");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Evaluaciones",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PersonaId",
                table: "ColegiosAlumnos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ColegiosAlumnos_AspNetUsers_PersonaId",
                table: "ColegiosAlumnos",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColegiosAlumnos_AspNetUsers_PersonaId",
                table: "ColegiosAlumnos");
            
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Evaluaciones");

            migrationBuilder.AlterColumn<string>(
                name: "PersonaId",
                table: "ColegiosAlumnos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_ColegiosAlumnos_AspNetUsers_PersonaId",
                table: "ColegiosAlumnos",
                column: "PersonaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
