using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class claseAlumnoColegio : Migration
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
                keyValue: "e4c8c216-d986-40c5-93e3-600f284a7958");*/

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

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ad0ff24-56a6-4475-9353-39843e489e98", "748cf853-cf7e-4f59-beb4-048e274efbc4", "Alumno", "ALUMNO" },
                    { "2ac32543-9f31-49b2-b2ae-c9ff71457067", "3474080e-03d8-4a82-89a7-7db57ba42ab1", "Master", "MASTER" },
                    { "9fff4624-1ef6-4cd9-a17a-ce667c6d2b33", "d7e3f724-b756-46dd-a141-4f9ca9dcefca", "Profesor", "PROFESOR" },
                    { "c798f617-3e82-4694-9227-951d31ebd0d3", "9fcd04a6-54ef-46dd-a680-d379a2fb072f", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4142e823-de82-4870-a6e8-719d7c486d68", 0, "Martinez", "0a403f15-180f-4784-b061-25304acbb16d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBm/J3+ek3NRQ/vxNavHgm39iTWb4aEQxIuMD9RMAsiVgaeNNS8y6bswOTSkqlREeQ==", null, false, "452758b9-1715-4f49-ba03-eec058067855", false, null },
                    { "95529008-dd05-40b0-8f70-a52bd9f0bb61", 0, "Torres", "1864725d-fb4b-4ba0-afcd-c796f89098a0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBzrh/Q2P8Z2Te/skAH42NEBh9r/YbRMzl8nABGs+ZRcyYyGc7yrPyCASjyxU7pLfA==", null, false, "db943655-ba49-4645-93b9-e9210675ca27", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c798f617-3e82-4694-9227-951d31ebd0d3", "4142e823-de82-4870-a6e8-719d7c486d68" },
                    { "2ac32543-9f31-49b2-b2ae-c9ff71457067", "95529008-dd05-40b0-8f70-a52bd9f0bb61" }
                });
            */
            /*migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId",
                unique: true);
            */
            migrationBuilder.CreateIndex(
                name: "IX_ClasesAlumnosColegios_ClaseId",
                table: "ClasesAlumnosColegios",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClasesAlumnosColegios_ColegiosAlumnosId",
                table: "ClasesAlumnosColegios",
                column: "ColegiosAlumnosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "ClasesAlumnosColegios");

            migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ad0ff24-56a6-4475-9353-39843e489e98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fff4624-1ef6-4cd9-a17a-ce667c6d2b33");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c798f617-3e82-4694-9227-951d31ebd0d3", "4142e823-de82-4870-a6e8-719d7c486d68" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ac32543-9f31-49b2-b2ae-c9ff71457067", "95529008-dd05-40b0-8f70-a52bd9f0bb61" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ac32543-9f31-49b2-b2ae-c9ff71457067");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c798f617-3e82-4694-9227-951d31ebd0d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4142e823-de82-4870-a6e8-719d7c486d68");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95529008-dd05-40b0-8f70-a52bd9f0bb61");
            */
            /*migrationBuilder.InsertData(
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
                });*/

            migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId");
        }
    }
}
