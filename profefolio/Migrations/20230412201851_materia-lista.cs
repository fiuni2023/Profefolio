using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class materialista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaLista_Clases_ClaseId",
                table: "MateriaLista");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MateriaLista",
                table: "MateriaLista");

            migrationBuilder.RenameTable(
                name: "MateriaLista",
                newName: "MateriaListas");

            migrationBuilder.RenameIndex(
                name: "IX_MateriaLista_ClaseId",
                table: "MateriaListas",
                newName: "IX_MateriaListas_ClaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MateriaListas",
                table: "MateriaListas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaListas_Clases_ClaseId",
                table: "MateriaListas",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriaListas_Clases_ClaseId",
                table: "MateriaListas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MateriaListas",
                table: "MateriaListas");

            migrationBuilder.RenameTable(
                name: "MateriaListas",
                newName: "MateriaLista");

            migrationBuilder.RenameIndex(
                name: "IX_MateriaListas_ClaseId",
                table: "MateriaLista",
                newName: "IX_MateriaLista_ClaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MateriaLista",
                table: "MateriaLista",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriaLista_Clases_ClaseId",
                table: "MateriaLista",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
