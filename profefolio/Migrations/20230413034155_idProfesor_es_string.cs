using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class idProfesor_es_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "ProfesorId",
                table: "MateriaListas",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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
            /*migrationBuilder.DropForeignKey(
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
            */
            migrationBuilder.AlterColumn<int>(
                name: "ProfesorId",
                table: "MateriaListas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            /*migrationBuilder.InsertData(
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
                */
        }
    }
}
