using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddModelEventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cb8c4c8-dd0b-41b1-8411-386c00126848");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e17270b4-4e5d-4c61-b33c-c5845652223f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "3bc82905-089c-4501-9bf8-80ac7ae2ded3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42d34633-dd40-4337-8e8a-0f3c50c8afcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5becbfc3-23e1-4a3c-8eef-1f7287bc168d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bc82905-089c-4501-9bf8-80ac7ae2ded3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f");

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MateriaId = table.Column<int>(type: "integer", nullable: false),
                    ClaseId = table.Column<int>(type: "integer", nullable: false),
                    ColegioId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Colegios_ColegioId",
                        column: x => x.ColegioId,
                        principalTable: "Colegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1dba6e2c-8d58-4964-9128-9acbe7c75fa5", "ab3654ed-7a6d-464f-85f9-8c1583e3b62e", "Profesor", "PROFESOR" },
                    { "36fb032c-48c2-4476-95b1-253ee5e4f09a", "4b96278d-6c59-46b2-8ed0-6ebb8b596f2a", "Alumno", "ALUMNO" },
                    { "5c22c564-7c7c-4c1a-965c-939f3179a9d6", "e9deb2fc-cc37-42b6-843b-5ae32ef99292", "Master", "MASTER" },
                    { "ffe510b7-8e32-43a0-84bb-68772596cfe7", "6e447923-051f-4e5d-a887-9f54e3dd5832", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e076efea-5861-4f7f-808a-2dc93c96dd70", 0, "Martinez", "5168a7e8-8f29-48c8-b94b-ee320b86cf95", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAECmhSdreJr8vUo6ZHo0OKs4u88aA6PmCL6Ng5M0bkrsBEg67tIZpm7gF0+NNSYi/PQ==", null, false, "e85fa8da-1af1-4d00-bb7c-4d8422cbc6c8", false, null },
                    { "e0a2f2fa-6184-4f66-9aeb-d36b67aeaf83", 0, "Torres", "42782cf4-ecd2-4d92-b370-79e67cedd24f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEO1GK8BYsGb7klDNHaRDcCgu6kwwKykfwgpxz2dS7crgron1/oQ5S3/tCAFWWWbMqQ==", null, false, "9f895ea9-0526-40fa-b366-476f1151b30b", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ffe510b7-8e32-43a0-84bb-68772596cfe7", "e076efea-5861-4f7f-808a-2dc93c96dd70" },
                    { "5c22c564-7c7c-4c1a-965c-939f3179a9d6", "e0a2f2fa-6184-4f66-9aeb-d36b67aeaf83" }
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
                name: "IX_Eventos_MateriaId",
                table: "Eventos",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dba6e2c-8d58-4964-9128-9acbe7c75fa5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36fb032c-48c2-4476-95b1-253ee5e4f09a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ffe510b7-8e32-43a0-84bb-68772596cfe7", "e076efea-5861-4f7f-808a-2dc93c96dd70" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5c22c564-7c7c-4c1a-965c-939f3179a9d6", "e0a2f2fa-6184-4f66-9aeb-d36b67aeaf83" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c22c564-7c7c-4c1a-965c-939f3179a9d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe510b7-8e32-43a0-84bb-68772596cfe7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e076efea-5861-4f7f-808a-2dc93c96dd70");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e0a2f2fa-6184-4f66-9aeb-d36b67aeaf83");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "b1c31710-2896-4516-84b6-9457fbb00ac4", "Master", "MASTER" },
                    { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "3e77738d-b274-4987-9d0e-b049e2efe0d4", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "7cb8c4c8-dd0b-41b1-8411-386c00126848", "1990aaab-0732-46bc-b121-d813d6e1434d", "Alumno", "ALUMNO" },
                    { "e17270b4-4e5d-4c61-b33c-c5845652223f", "36b805ca-736e-4b7d-8f89-5f7de26525eb", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3bc82905-089c-4501-9bf8-80ac7ae2ded3", 0, "Torres", "8379a705-3049-4217-8fc3-b15262d57172", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEK75pSDQ/V91u2/yRr71hZED5dfg6qnYeuc3Z5wU95eHRE66hD9U8zlUCUg2FxgNhg==", null, false, "27804b21-5b81-4c3e-af9b-4b6addcd8c0f", false, null },
                    { "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f", 0, "Martinez", "364a0b7f-e403-4c48-81e6-425bc1ccc4e4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEICvEyqsGD5mG5E3du+i1lGvTjxucPedmuCGduy9J7rKUVzAhaPgywTr5AaLWwqGgA==", null, false, "ec28fb68-8fb3-40c1-959a-1592a7f40383", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "3bc82905-089c-4501-9bf8-80ac7ae2ded3" },
                    { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f" }
                });
        }
    }
}
