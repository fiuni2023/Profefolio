using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class init : Migration
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02a87398-71f1-4ecd-9a26-3cae8f3e8e68", "a8bbf26e-8f12-4bb4-8651-3c0f55956b28", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "16c49fa9-a5ef-4ca8-a9a7-b682f34b5a7d", "ced34c5d-66f8-414a-bef6-3cee579b42d0", "Profesor", "PROFESOR" },
                    { "90102b0c-351d-4a6c-8652-11ceeb521979", "6130b0aa-fc3e-4303-ae9e-43029ff96068", "Master", "MASTER" },
                    { "90ee9255-8ec2-42e1-acb5-23536a074335", "b2160811-c4da-41aa-82aa-a41c4ee5b2d5", "Alumno", "ALUMNO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "862b82ff-66b2-4585-a57d-51d421c4ec1b", 0, "Martinez", "f55004c7-6e8a-416a-a90b-1533bb3bb8e4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAECNf3u7R8HigPBl5RDb2d+PcEjqHwrcXc16qGhiYzdXgFZKaMwcJNLJRGUI2lqXQ2A==", null, false, "1b71d8a3-ca6a-4c5c-abac-e75d36d375ff", false, null },
                    { "a4d22365-69b0-4863-b900-c47c46abdbeb", 0, "Torres", "9d638471-5830-40e0-a48e-7c980a9a0b96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEH+Zj6u9hHk9m9fSiODKto3oBQA9NYNBU2NFpjp6QMM/JFsONEIMVzJoIpy/EBrNWg==", null, false, "ff53ddb9-df88-4226-9f58-82659a960899", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "02a87398-71f1-4ecd-9a26-3cae8f3e8e68", "862b82ff-66b2-4585-a57d-51d421c4ec1b" },
                    { "90102b0c-351d-4a6c-8652-11ceeb521979", "a4d22365-69b0-4863-b900-c47c46abdbeb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios",
                column: "PersonaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colegios_PersonaId",
                table: "Colegios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16c49fa9-a5ef-4ca8-a9a7-b682f34b5a7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90ee9255-8ec2-42e1-acb5-23536a074335");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02a87398-71f1-4ecd-9a26-3cae8f3e8e68", "862b82ff-66b2-4585-a57d-51d421c4ec1b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "90102b0c-351d-4a6c-8652-11ceeb521979", "a4d22365-69b0-4863-b900-c47c46abdbeb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02a87398-71f1-4ecd-9a26-3cae8f3e8e68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90102b0c-351d-4a6c-8652-11ceeb521979");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "862b82ff-66b2-4585-a57d-51d421c4ec1b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4d22365-69b0-4863-b900-c47c46abdbeb");

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
