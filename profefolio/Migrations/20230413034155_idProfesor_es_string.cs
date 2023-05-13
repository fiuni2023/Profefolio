using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class idProfesor_es_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
           

            migrationBuilder.AlterColumn<string>(
                name: "ProfesorId",
                table: "MateriaListas",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaListas_MateriaId",
                table: "MateriaListas",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaListas_ProfesorId",
                table: "MateriaListas",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaListas_AspNetUsers_ProfesorId",
                table: "MateriaListas",
                column: "ProfesorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaListas_Materias_MateriaId",
                table: "MateriaListas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaListas_AspNetUsers_ProfesorId",
                table: "MateriaListas");

            migrationBuilder.DropForeignKey(
                name: "FK_MateriaListas_Materias_MateriaId",
                table: "MateriaListas");

            migrationBuilder.DropIndex(
                name: "IX_MateriaListas_MateriaId",
                table: "MateriaListas");

            migrationBuilder.DropIndex(
                name: "IX_MateriaListas_ProfesorId",
                table: "MateriaListas");

           
            migrationBuilder.AlterColumn<int>(
                name: "ProfesorId",
                table: "MateriaListas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

        }
    }
}
