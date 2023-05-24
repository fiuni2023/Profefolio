using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddAsistencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71dfe3a3-7c45-4689-8581-83ef7ed896eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "885b6250-24bb-49ec-a2e3-1bf1f015dc95");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "54f1815c-ce34-446f-994b-43708d2eea5e", "ae131863-3b79-4a48-b028-311cd4c41515" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0255d4fb-2df0-47b0-97cc-96a00ad47449", "dc8135b1-c453-4c9f-b5ed-c53a56198c45" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0255d4fb-2df0-47b0-97cc-96a00ad47449");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54f1815c-ce34-446f-994b-43708d2eea5e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ae131863-3b79-4a48-b028-311cd4c41515");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dc8135b1-c453-4c9f-b5ed-c53a56198c45");*/

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClasesAlumnosColegioId = table.Column<int>(type: "integer", nullable: false),
                    MateriaListaId = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<char>(type: "character(1)", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_ClasesAlumnosColegios_ClasesAlumnosColegioId",
                        column: x => x.ClasesAlumnosColegioId,
                        principalTable: "ClasesAlumnosColegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_MateriaListas_MateriaListaId",
                        column: x => x.MateriaListaId,
                        principalTable: "MateriaListas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3998cbe4-3b74-4019-ac2f-672e99c8c145", "1fbbf43d-7094-4f35-a7d1-871c248ed09f", "Alumno", "ALUMNO" },
                    { "822ff007-98ec-4afd-a26e-79d1759ff010", "34b15c78-bbc2-4c25-b7c5-193f819b46c9", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "8a025f97-cec9-46f5-8d26-974daa8958da", "44422308-1b5f-4ac8-8abc-5a26171dff1e", "Profesor", "PROFESOR" },
                    { "d9732d5c-ab9d-49f2-ab7c-c031eef5b986", "ca112ca7-d157-4f9b-8ac5-ee2fdfa66455", "Master", "MASTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "89f65ce0-361e-4a91-9b44-61957dfdf5aa", 0, "Martinez", "e474c9b4-a766-4ab5-a06b-b66fc4141ea3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAELvlTZpnIAq3TQF39em218VPJEyo4c+9dCLVxXldywr2Ok4WbZeOlTICYevVTmdW5A==", null, false, "46819ae8-0ff7-4fb2-b692-c294b27e41d2", false, null },
                    { "cd57d62a-e536-470e-b7d3-133ea639cbcd", 0, "Torres", "4b64bcef-77e9-4237-9fc2-ba84366b2357", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOT5T1ujPcrJInZ9t8Cb7P0Jum6QnI6KuhARRr9xasIgzj2eJ7d+eeIShSFD3qtBFw==", null, false, "4671406c-70de-4557-aab1-f7d728b13524", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "822ff007-98ec-4afd-a26e-79d1759ff010", "89f65ce0-361e-4a91-9b44-61957dfdf5aa" },
                    { "d9732d5c-ab9d-49f2-ab7c-c031eef5b986", "cd57d62a-e536-470e-b7d3-133ea639cbcd" }
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_ClasesAlumnosColegioId",
                table: "Asistencias",
                column: "ClasesAlumnosColegioId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_MateriaListaId",
                table: "Asistencias",
                column: "MateriaListaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            /*migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3998cbe4-3b74-4019-ac2f-672e99c8c145");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a025f97-cec9-46f5-8d26-974daa8958da");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "822ff007-98ec-4afd-a26e-79d1759ff010", "89f65ce0-361e-4a91-9b44-61957dfdf5aa" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d9732d5c-ab9d-49f2-ab7c-c031eef5b986", "cd57d62a-e536-470e-b7d3-133ea639cbcd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "822ff007-98ec-4afd-a26e-79d1759ff010");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9732d5c-ab9d-49f2-ab7c-c031eef5b986");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89f65ce0-361e-4a91-9b44-61957dfdf5aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd57d62a-e536-470e-b7d3-133ea639cbcd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0255d4fb-2df0-47b0-97cc-96a00ad47449", "dc1b6521-cc44-41ed-b9e8-069864ce0306", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "54f1815c-ce34-446f-994b-43708d2eea5e", "57fcda6c-6881-4b86-a3f9-2560bdcca064", "Master", "MASTER" },
                    { "71dfe3a3-7c45-4689-8581-83ef7ed896eb", "005381db-8b2c-435b-96fa-483d83d385ba", "Alumno", "ALUMNO" },
                    { "885b6250-24bb-49ec-a2e3-1bf1f015dc95", "81996f61-5ec8-44d8-83b7-4f60f5144e68", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ae131863-3b79-4a48-b028-311cd4c41515", 0, "Torres", "d16488f0-5a11-477e-b24d-88b2b1caa744", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEC9Oj+qe1aB8HB1yT+e442hfwhNb9XcQzZwthgsnhUSamPssYDRyWEQcFsc0eWTOow==", null, false, "83153b43-52c9-4751-acf3-afebebc563a9", false, null },
                    { "dc8135b1-c453-4c9f-b5ed-c53a56198c45", 0, "Martinez", "63d2309f-0700-40b8-850c-ffef82c536c8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAELjlXO+bmuCeH5Jw9RM561C59lpFm9+JNV/GMTjrOclFoXdKEcbVFQ1ZfPADo0DcZA==", null, false, "05ecd5e4-ed3a-4fe1-b2b5-b06dc21c77dd", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "54f1815c-ce34-446f-994b-43708d2eea5e", "ae131863-3b79-4a48-b028-311cd4c41515" },
                    { "0255d4fb-2df0-47b0-97cc-96a00ad47449", "dc8135b1-c453-4c9f-b5ed-c53a56198c45" }
                });*/
        }
    }
}
