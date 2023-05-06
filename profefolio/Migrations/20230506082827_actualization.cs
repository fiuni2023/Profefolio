using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class actualization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50b44ece-2c83-4a7f-82de-877319f33e12", "6e8e6b00-6b30-4c59-98da-63c7af99f094", "Profesor", "PROFESOR" },
                    { "aa58f7ce-5895-4d6f-a2e8-60dfc04774ee", "730decd7-ebcf-40f2-a9af-7096446647e0", "Master", "MASTER" },
                    { "b74acecb-66a4-4324-b211-daf3748c7343", "bcf3366b-9738-4c00-a665-73035bc845bd", "Alumno", "ALUMNO" },
                    { "cdc38e1e-7446-4fe3-8aac-c7ccb52acd00", "453132a2-a09a-4c7f-9b4d-65ae9d6e9eda", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9bb3cd86-6571-490d-b234-59a117e20876", 0, "Torres", "a1dd8db7-603c-4bce-9eb0-b571e15b8bfa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEGoiia+fH2QOuNwSmU/7hi+bZfb1IqOzQnQKXRJU/bdmciKsBunAajKLK+7/uqMQoQ==", null, false, "04840591-480c-47e5-a104-fd09922aac29", false, null },
                    { "e57a9121-af85-4f30-82d0-b309086378e3", 0, "Martinez", "24c0b670-ff3d-4cd9-9bf1-757e6a5ad055", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOXXp9LXCs0GrbIdbVhazYn2+TXejCdOeOb0fviqFmI25uVES+CxdWzny3cQEGJwVQ==", null, false, "d17d562a-25ef-4426-b649-e5775d16a8f7", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "aa58f7ce-5895-4d6f-a2e8-60dfc04774ee", "9bb3cd86-6571-490d-b234-59a117e20876" },
                    { "cdc38e1e-7446-4fe3-8aac-c7ccb52acd00", "e57a9121-af85-4f30-82d0-b309086378e3" }
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
                column: "ColegiosAlumnosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasesAlumnosColegios");

            migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50b44ece-2c83-4a7f-82de-877319f33e12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b74acecb-66a4-4324-b211-daf3748c7343");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "aa58f7ce-5895-4d6f-a2e8-60dfc04774ee", "9bb3cd86-6571-490d-b234-59a117e20876" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cdc38e1e-7446-4fe3-8aac-c7ccb52acd00", "e57a9121-af85-4f30-82d0-b309086378e3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa58f7ce-5895-4d6f-a2e8-60dfc04774ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdc38e1e-7446-4fe3-8aac-c7ccb52acd00");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bb3cd86-6571-490d-b234-59a117e20876");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e57a9121-af85-4f30-82d0-b309086378e3");

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
                column: "PersonaId");
        }
    }
}
