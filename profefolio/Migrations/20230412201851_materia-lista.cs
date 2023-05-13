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

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63cd5cb2-fe1e-431c-a16d-400367961c4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1f74249-35f0-4666-b196-76e1cebd9872");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27df30ef-7db0-4e2f-9cc1-802159a49306", "e7fcfdba-8419-4239-92a3-f425aa62de49" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e2ee620b-23d1-47b0-b116-4cb1a3e77896", "edb72cd3-6181-4aae-9568-b69c60ce1796" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27df30ef-7db0-4e2f-9cc1-802159a49306");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2ee620b-23d1-47b0-b116-4cb1a3e77896");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e7fcfdba-8419-4239-92a3-f425aa62de49");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edb72cd3-6181-4aae-9568-b69c60ce1796");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1854d1d8-9316-4a92-8bc3-9dea6ef2b0e3", "9be5a42d-85a9-498b-9695-2c8b9169e39c", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "2b0d30b1-68ce-4afb-a626-95c05f670525", "5691b049-2499-4f88-964a-feb496f0897f", "Master", "MASTER" },
                    { "415eb454-2a5c-4ab0-8df2-453aa43e769b", "7547557b-a8fc-4d91-9e46-f12ae9fc3fb3", "Alumno", "ALUMNO" },
                    { "43700e4c-9a14-452d-aa88-b6ea97fd92de", "1f57b654-eaad-4b52-be8c-43fb50f3e64c", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2c0cd7c5-bce7-476a-9a81-75f84a34c3e7", 0, "Martinez", "61c7bd9c-ed55-439b-9119-2a46fc31cbe0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOsg7YEOwkYWYODXRQWiXZQCWkVnaIY3ehT+J6Q5o3cBOnb81FIYsjjVfUl23yXUpw==", null, false, "82220a46-0b86-4181-b0f3-be29e69d6eb9", false, null },
                    { "36803c9c-f213-4dc9-83ef-b58681f99e62", 0, "Torres", "b8ecfbea-b405-46a8-9c3c-9ce7a70ebdec", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBKp5R7aId9NodamcB0xtM6i0zjViFWGVTQmLRgJ438ysgEtVoaWKaScsSM4p2UoIA==", null, false, "58cf2a94-9765-4dba-a24c-d6bc47c51693", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1854d1d8-9316-4a92-8bc3-9dea6ef2b0e3", "2c0cd7c5-bce7-476a-9a81-75f84a34c3e7" },
                    { "2b0d30b1-68ce-4afb-a626-95c05f670525", "36803c9c-f213-4dc9-83ef-b58681f99e62" }
                });

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

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "415eb454-2a5c-4ab0-8df2-453aa43e769b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43700e4c-9a14-452d-aa88-b6ea97fd92de");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1854d1d8-9316-4a92-8bc3-9dea6ef2b0e3", "2c0cd7c5-bce7-476a-9a81-75f84a34c3e7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2b0d30b1-68ce-4afb-a626-95c05f670525", "36803c9c-f213-4dc9-83ef-b58681f99e62" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1854d1d8-9316-4a92-8bc3-9dea6ef2b0e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b0d30b1-68ce-4afb-a626-95c05f670525");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c0cd7c5-bce7-476a-9a81-75f84a34c3e7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36803c9c-f213-4dc9-83ef-b58681f99e62");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27df30ef-7db0-4e2f-9cc1-802159a49306", "aa986282-06e5-47ec-a00d-3420e123f0aa", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "63cd5cb2-fe1e-431c-a16d-400367961c4b", "842cdb42-e43a-41c8-95f6-c77e34123e62", "Alumno", "ALUMNO" },
                    { "c1f74249-35f0-4666-b196-76e1cebd9872", "15b40514-fa74-4001-bd96-fc1c725f7f02", "Profesor", "PROFESOR" },
                    { "e2ee620b-23d1-47b0-b116-4cb1a3e77896", "47deffce-466c-4788-a4b4-6f6e6e8e4040", "Master", "MASTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e7fcfdba-8419-4239-92a3-f425aa62de49", 0, "Martinez", "db46308c-fb6b-420f-ad63-854332dbcc04", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAELZh9zdQwhgWclr1xNC5vWHWtH1mOYVAg1QuaHHld1gAjeh6MEPGDMH6v7RKGF63sQ==", null, false, "db6c291c-e079-48a2-bbbd-0d2b581f032a", false, null },
                    { "edb72cd3-6181-4aae-9568-b69c60ce1796", 0, "Torres", "d9bb947e-7a38-4fac-b1c1-570fd1cdb7e5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOVF8cEkB1XggIlVfx4YG/hgHC29wKELjKhMYDS3dQmICjaH1LmzICSk9WG2DMHpAA==", null, false, "df5b278e-7490-4541-9305-fad1fc843c2c", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "27df30ef-7db0-4e2f-9cc1-802159a49306", "e7fcfdba-8419-4239-92a3-f425aa62de49" },
                    { "e2ee620b-23d1-47b0-b116-4cb1a3e77896", "edb72cd3-6181-4aae-9568-b69c60ce1796" }
                });

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
