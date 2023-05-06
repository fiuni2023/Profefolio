using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddModelHorasCatedras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /*migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27683e2e-3c43-431b-b8ac-02314ebd2d40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "347c6b56-5d1a-4e37-a526-6a2bf4dfb2c1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1", "511fd355-5b35-48e3-86f3-1ad9e99c1264" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "675f43a7-36c9-41d0-9d31-65ee4a40ffe4", "e4c8c216-d986-40c5-93e3-600f284a7958" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "675f43a7-36c9-41d0-9d31-65ee4a40ffe4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "511fd355-5b35-48e3-86f3-1ad9e99c1264");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4c8c216-d986-40c5-93e3-600f284a7958");

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
            */
            migrationBuilder.CreateTable(
                name: "HorasCatedras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inicio = table.Column<string>(type: "text", nullable: false),
                    Fin = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasCatedras", x => x.Id);
                });

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "9319a521-f625-4f5c-847c-b5f59f5fe7c4", "Master", "MASTER" },
                    { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "480d6cde-e30f-42f8-9b92-aea07e3b6420", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "345d7eef-591f-44a4-bc6d-d5ba8ad37007", "11b3534b-c6f8-41bd-bbbf-dcc149eb8be7", "Alumno", "ALUMNO" },
                    { "cdafa56c-bbff-4f2b-99a7-d531bd31edde", "6bfb0fa4-879c-419f-9b96-8d25e8c93c1f", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "de91e436-c9eb-487e-8149-e61209c326a3", 0, "Torres", "1811c8c5-4452-4956-aaa2-4902bb59b47f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAECnMeda8ocMej2xdIu+5yPJVxtJQFXDUcWmkSkLBG+EbqWT7d88XyQlQDl6oBQaiEA==", null, false, "8cd0710f-656a-432e-be1a-f806d99652e6", false, null },
                    { "e9fa2d90-d576-4e8b-9160-b78610e7414c", 0, "Martinez", "a29f0bf7-55be-4bd5-b3f3-c73776cde3c7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEDvNsJ6SchQoCcg31Xap/J4LvVHAbj6Jzzx3kr0uDvIQ63030OI5907sF0XlqMqGvQ==", null, false, "368be0a7-3377-49dd-8cd4-58fe875bbb4b", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "de91e436-c9eb-487e-8149-e61209c326a3" },
                    { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "e9fa2d90-d576-4e8b-9160-b78610e7414c" }
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
                column: "ColegiosAlumnosId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "ClasesAlumnosColegios");*/

            migrationBuilder.DropTable(
                name: "HorasCatedras");

            /*migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "345d7eef-591f-44a4-bc6d-d5ba8ad37007");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdafa56c-bbff-4f2b-99a7-d531bd31edde");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "de91e436-c9eb-487e-8149-e61209c326a3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "e9fa2d90-d576-4e8b-9160-b78610e7414c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21d4fef8-d68c-4b85-92e0-c1e44613e6a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de91e436-c9eb-487e-8149-e61209c326a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9fa2d90-d576-4e8b-9160-b78610e7414c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27683e2e-3c43-431b-b8ac-02314ebd2d40", "8f8fbe31-f1de-488f-89b4-9a50eabec092", "Profesor", "PROFESOR" },
                    { "347c6b56-5d1a-4e37-a526-6a2bf4dfb2c1", "ff5643fe-79c7-4ada-9416-68cd783344e5", "Alumno", "ALUMNO" },
                    { "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1", "967898bd-ced3-4cca-8183-18ae8b478315", "Master", "MASTER" },
                    { "675f43a7-36c9-41d0-9d31-65ee4a40ffe4", "e60d57af-daf0-4ce0-b242-0bd50c53ec2e", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "511fd355-5b35-48e3-86f3-1ad9e99c1264", 0, "Torres", "893ec1c6-1890-4db5-9e92-6db47bd663d8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOkSredtQO/Tq9ub0o9kFSBdv4IyXjyC+JAzzVq9D7+4fBE6Xl5E52ksrKCN7kI7cg==", null, false, "ff2ee1a5-9361-41aa-a75b-0e2f7e4ea55a", false, null },
                    { "e4c8c216-d986-40c5-93e3-600f284a7958", 0, "Martinez", "bea1738d-3956-48df-aa63-93433ed4c9b4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEAILkdPJTmgi/EHDH/ID//TQ54RC2SWmcSUJZiuPoq5g6V1QPFp+VfMz+PJEC++aJw==", null, false, "cc1269f9-4110-4f7c-9a7b-d975348a368c", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1", "511fd355-5b35-48e3-86f3-1ad9e99c1264" },
                    { "675f43a7-36c9-41d0-9d31-65ee4a40ffe4", "e4c8c216-d986-40c5-93e3-600f284a7958" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId");*/
        }
    }
}
