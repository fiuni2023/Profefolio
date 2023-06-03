using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class dropcalificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventoAlumnos_Calificaciones_CalificacionId",
                table: "EventoAlumnos");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_EventoAlumnos_CalificacionId",
                table: "EventoAlumnos");

           
            migrationBuilder.DropColumn(
                name: "CalificacionId",
                table: "EventoAlumnos");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "CalificacionId",
                table: "EventoAlumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    PorcentajeLogrado = table.Column<double>(type: "double precision", nullable: false),
                    PorcentajeTotal = table.Column<double>(type: "double precision", nullable: false),
                    PuntajeLogrado = table.Column<double>(type: "double precision", nullable: false),
                    PuntajeTotal = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65be3f33-e558-4945-a4fc-c55af43b385f", "92860d9b-9f54-4015-ac1f-88dc6c711462", "Alumno", "ALUMNO" },
                    { "710a0604-b124-44dd-a65a-8494f59b7e58", "0c79dc98-3d48-41a4-9fe7-2a3da79cf196", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "c42dbf0f-8b42-4e5b-a5a6-eb99c2a7c292", "572e6021-bfdc-4e06-9396-1ce62d23e116", "Profesor", "PROFESOR" },
                    { "d5b81399-8466-40fa-a28d-2cede0e0d36c", "93dc493e-efff-4083-be7c-f5fbb98164bc", "Master", "MASTER" }
                });
            migrationBuilder.CreateIndex(
                name: "IX_EventoAlumnos_CalificacionId",
                table: "EventoAlumnos",
                column: "CalificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventoAlumnos_Calificaciones_CalificacionId",
                table: "EventoAlumnos",
                column: "CalificacionId",
                principalTable: "Calificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
