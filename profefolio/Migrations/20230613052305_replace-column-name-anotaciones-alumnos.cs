using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class replacecolumnnameanotacionesalumnos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnotacionesAlumnos_MateriaListas_MateriaListaId",
                table: "AnotacionesAlumnos");

            /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b95beed-f1cc-461c-aa70-2def1e972ba7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e7180ac-a3ae-46a8-ae9c-8789772c9049");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "53de91f4-22e2-45ee-a452-3e286e02306c", "743d632f-1d51-4551-af64-c5c9f7787cda" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cb978130-3f14-4c56-a8d9-3dce0eae20f9", "7918ffe0-1afe-4bf3-88bf-dc8d7b7c2c5f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53de91f4-22e2-45ee-a452-3e286e02306c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb978130-3f14-4c56-a8d9-3dce0eae20f9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "743d632f-1d51-4551-af64-c5c9f7787cda");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7918ffe0-1afe-4bf3-88bf-dc8d7b7c2c5f");
 */
            migrationBuilder.RenameColumn(
                name: "MateriaListaId",
                table: "AnotacionesAlumnos",
                newName: "ClaseId");

            migrationBuilder.RenameIndex(
                name: "IX_AnotacionesAlumnos_MateriaListaId",
                table: "AnotacionesAlumnos",
                newName: "IX_AnotacionesAlumnos_ClaseId");

/*             migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5332981c-15f6-48d2-b9b3-8c5cf9c25d4a", "820ff730-4f40-4b88-8b7d-3f748e8fe84e", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "53c19baf-65f2-49a7-8773-99247d7ea43d", "00f716fb-9912-4d5c-965e-39659863eed9", "Alumno", "ALUMNO" },
                    { "b3dfd007-2fc6-478c-acc1-7c5350add1db", "e358390c-97ff-46d8-9b17-739e7d9424c7", "Profesor", "PROFESOR" },
                    { "c97e1301-347d-4294-ad47-5f0df327aa3e", "f9a3758b-84cd-4d83-ae1e-c1704798f292", "Master", "MASTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2d0aaa6b-2f26-4ffc-8361-22e0a0867a0a", 0, "Torres", "de77d173-7324-4c5b-929b-0dabb72d53f4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEGwJ8dJIcmJulW4/Ht7e3lNAIwIkyQkXjx2my0TO9ps5rD73aLijZHd0rOLDBh/6DA==", null, false, "d243a342-3b59-4e6b-863e-0ee952af3267", false, null },
                    { "63d0c5a1-687b-4acb-893c-1f67408cf1dc", 0, "Martinez", "6d8d2014-4d0e-4983-862f-dee8d81d11c4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAENRykGDNYHM4czV+hCvKg4rbCbSFR6ZG90yY2UnOpCwnrRnDa5lBWmOH5Bdx9F/JTw==", null, false, "15b8c6bd-6162-4d2d-9453-30effa5b81d8", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c97e1301-347d-4294-ad47-5f0df327aa3e", "2d0aaa6b-2f26-4ffc-8361-22e0a0867a0a" },
                    { "5332981c-15f6-48d2-b9b3-8c5cf9c25d4a", "63d0c5a1-687b-4acb-893c-1f67408cf1dc" }
                });

 */            migrationBuilder.AddForeignKey(
                name: "FK_AnotacionesAlumnos_Clases_ClaseId",
                table: "AnotacionesAlumnos",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnotacionesAlumnos_Clases_ClaseId",
                table: "AnotacionesAlumnos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53c19baf-65f2-49a7-8773-99247d7ea43d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3dfd007-2fc6-478c-acc1-7c5350add1db");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c97e1301-347d-4294-ad47-5f0df327aa3e", "2d0aaa6b-2f26-4ffc-8361-22e0a0867a0a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5332981c-15f6-48d2-b9b3-8c5cf9c25d4a", "63d0c5a1-687b-4acb-893c-1f67408cf1dc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5332981c-15f6-48d2-b9b3-8c5cf9c25d4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c97e1301-347d-4294-ad47-5f0df327aa3e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d0aaa6b-2f26-4ffc-8361-22e0a0867a0a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63d0c5a1-687b-4acb-893c-1f67408cf1dc");

            migrationBuilder.RenameColumn(
                name: "ClaseId",
                table: "AnotacionesAlumnos",
                newName: "MateriaListaId");

            migrationBuilder.RenameIndex(
                name: "IX_AnotacionesAlumnos_ClaseId",
                table: "AnotacionesAlumnos",
                newName: "IX_AnotacionesAlumnos_MateriaListaId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53de91f4-22e2-45ee-a452-3e286e02306c", "5f3cae04-ede6-4682-9e1b-5830d8fb478f", "Master", "MASTER" },
                    { "5b95beed-f1cc-461c-aa70-2def1e972ba7", "409f1ad8-b7f6-43ad-a551-fa63f1d9f6c6", "Alumno", "ALUMNO" },
                    { "5e7180ac-a3ae-46a8-ae9c-8789772c9049", "f75b0ddc-3bc3-402b-932a-c788b08a383c", "Profesor", "PROFESOR" },
                    { "cb978130-3f14-4c56-a8d9-3dce0eae20f9", "782d5569-1646-4f8d-91b3-a640d4d75270", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "743d632f-1d51-4551-af64-c5c9f7787cda", 0, "Torres", "cb1aa5e2-c3ff-498e-a7be-61fdabd01800", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEL/XeIY0Y1D1gGS6ISx4GRdr5Y+NnZII7gp3SHQOBmNa0Kb1WMuGP7vE4Cgbe96Lfw==", null, false, "1f24651d-393f-4223-9b1e-ba4153ea508b", false, null },
                    { "7918ffe0-1afe-4bf3-88bf-dc8d7b7c2c5f", 0, "Martinez", "dd0d352f-e1e0-4a5f-b5c0-8f26200dc0fb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEP1PDzKh8FtmdG+GRNJKnS5lbiNR1pJ0aHnW8UlNbAyy4YZCukAYrLRgbHJK7VnXUA==", null, false, "2c3f51c9-1fac-4d55-9d07-bb4d967cf54e", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "53de91f4-22e2-45ee-a452-3e286e02306c", "743d632f-1d51-4551-af64-c5c9f7787cda" },
                    { "cb978130-3f14-4c56-a8d9-3dce0eae20f9", "7918ffe0-1afe-4bf3-88bf-dc8d7b7c2c5f" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AnotacionesAlumnos_MateriaListas_MateriaListaId",
                table: "AnotacionesAlumnos",
                column: "MateriaListaId",
                principalTable: "MateriaListas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
