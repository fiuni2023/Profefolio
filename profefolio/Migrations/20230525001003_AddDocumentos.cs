using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddDocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DeleteData(
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
            */
            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Enlace = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProfesorId = table.Column<string>(type: "text", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                });
            /*
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2dc0b193-ca3b-478e-878a-4f4bd2bb1895", "a24e674f-58dd-4b11-8160-bb88bb9f2a73", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "6ae1ea1e-b1f1-459a-ac95-9c35b863ad35", "776b6bbe-2fef-440c-88fe-baf43dab6fa2", "Master", "MASTER" },
                    { "b96fd3a2-43d6-4f6c-bab0-d64063ecf89c", "c186d425-96e1-4ef3-a6d6-3fdba1ea3b80", "Profesor", "PROFESOR" },
                    { "cff61775-c4b1-450a-9ab2-997cd08cb222", "a4f83893-6035-474d-b75b-c5fb5cdc9433", "Alumno", "ALUMNO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "09a32011-f8b9-4558-845e-199afd63f15b", 0, "Torres", "89f2a49c-82ac-4cea-9bde-3c07ab45a5d3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAECTp5K5133E9MQ1C/AI29FUNPo0UFD5aLX2EVrEwc3LimMV40We3TZtUpYK2FimfmQ==", null, false, "8c89c3d0-9cc9-4ee7-8cb0-1d3711fb051c", false, null },
                    { "afb3aa4e-3645-4cd6-920c-c43865034d32", 0, "Martinez", "48d624ac-a676-449e-b049-885b6a40b7a5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEEDavO7Loyx6hTCOggH++fZXRMc+20K0IY7FzFkBa1p21A7Wqdi+yFBYrFPpW4t4Tg==", null, false, "7efceff2-8255-45c2-a5f2-5516c56be33b", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6ae1ea1e-b1f1-459a-ac95-9c35b863ad35", "09a32011-f8b9-4558-845e-199afd63f15b" },
                    { "2dc0b193-ca3b-478e-878a-4f4bd2bb1895", "afb3aa4e-3645-4cd6-920c-c43865034d32" }
                });
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b96fd3a2-43d6-4f6c-bab0-d64063ecf89c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cff61775-c4b1-450a-9ab2-997cd08cb222");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6ae1ea1e-b1f1-459a-ac95-9c35b863ad35", "09a32011-f8b9-4558-845e-199afd63f15b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2dc0b193-ca3b-478e-878a-4f4bd2bb1895", "afb3aa4e-3645-4cd6-920c-c43865034d32" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dc0b193-ca3b-478e-878a-4f4bd2bb1895");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ae1ea1e-b1f1-459a-ac95-9c35b863ad35");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09a32011-f8b9-4558-845e-199afd63f15b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afb3aa4e-3645-4cd6-920c-c43865034d32");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
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
                });
            */
        }
    }
}
