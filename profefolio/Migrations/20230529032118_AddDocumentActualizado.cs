using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddDocumentActualizado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c630afd-45dc-4a2d-9f38-df661b70e332");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2c1e41d-d148-4656-9127-866a34ac3a81");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ddc37f20-ed05-4c33-8851-3231bc2d5461", "283e9c43-e812-4a43-8947-0bc6b66bc8d9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cfafd31e-7726-4462-b80a-3a04ec6dfbd7", "508d1452-6c15-4266-99db-67cd58e42063" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfafd31e-7726-4462-b80a-3a04ec6dfbd7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddc37f20-ed05-4c33-8851-3231bc2d5461");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "283e9c43-e812-4a43-8947-0bc6b66bc8d9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "508d1452-6c15-4266-99db-67cd58e42063");
            */
            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Documentos");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Documentos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Enlace",
                table: "Documentos",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
            /*
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "343f3bc4-cc24-4493-a6d3-21a4a0ffb795", "d66068f4-7654-4ab4-91f4-f7e3acb12fea", "Master", "MASTER" },
                    { "663ae2d9-ea36-49c9-a642-54a1c0444c06", "14e5c30b-fad7-4837-9f3f-a94f113950d1", "Alumno", "ALUMNO" },
                    { "ba86256b-f990-4c64-9718-fd3a12ecfce2", "0be05713-af39-47f1-8008-8db5a836b126", "Profesor", "PROFESOR" },
                    { "ed00b966-b749-4abb-8efd-6703d4fb691d", "68b6ebaa-963b-4f03-85a5-97691728442f", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c9706138-d1c6-4aab-b94c-9c9aba0925c4", 0, "Martinez", "45106a76-2123-40fe-b715-dddeb1e4309f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBDtvPpMKLh6fBJbT/xzG7/xgIeECX1xvjPh0nbswn5dVMW2JRcacS/5b9EgGoN0YQ==", null, false, "438dd245-280b-4172-ad8c-11c543f6f9f8", false, null },
                    { "eb0772ca-b805-49fb-8ebf-1442977c828a", 0, "Torres", "8e3b0658-eb70-4867-a3c0-d607cd503657", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEHf+PyvfCph1smxFy9DxOG+DthSDjk42ke0sPsbg7I0XmxKSBbVLGV9aWd6Ee5ilGg==", null, false, "c5024530-db08-43e1-8626-86d3c1f37537", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ed00b966-b749-4abb-8efd-6703d4fb691d", "c9706138-d1c6-4aab-b94c-9c9aba0925c4" },
                    { "343f3bc4-cc24-4493-a6d3-21a4a0ffb795", "eb0772ca-b805-49fb-8ebf-1442977c828a" }
                });
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663ae2d9-ea36-49c9-a642-54a1c0444c06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba86256b-f990-4c64-9718-fd3a12ecfce2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ed00b966-b749-4abb-8efd-6703d4fb691d", "c9706138-d1c6-4aab-b94c-9c9aba0925c4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "343f3bc4-cc24-4493-a6d3-21a4a0ffb795", "eb0772ca-b805-49fb-8ebf-1442977c828a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "343f3bc4-cc24-4493-a6d3-21a4a0ffb795");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed00b966-b749-4abb-8efd-6703d4fb691d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9706138-d1c6-4aab-b94c-9c9aba0925c4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb0772ca-b805-49fb-8ebf-1442977c828a");
            */
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Documentos",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Enlace",
                table: "Documentos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

           /* migrationBuilder.AddColumn<string>(
                name: "ProfesorId",
                table: "Documentos",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c630afd-45dc-4a2d-9f38-df661b70e332", "02593377-726d-4f93-9b49-874d31016788", "Profesor", "PROFESOR" },
                    { "b2c1e41d-d148-4656-9127-866a34ac3a81", "113247ca-e643-43d1-82c2-6590673ee9bd", "Alumno", "ALUMNO" },
                    { "cfafd31e-7726-4462-b80a-3a04ec6dfbd7", "9785bf68-bcc3-42ef-898a-461645e7965b", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "ddc37f20-ed05-4c33-8851-3231bc2d5461", "a63b36b6-ed73-4950-8522-e1e7aac59be2", "Master", "MASTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "283e9c43-e812-4a43-8947-0bc6b66bc8d9", 0, "Torres", "5ae61b2b-d5ed-440c-b156-620aeb98d5a9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBtzOlgrqsus2MSgi4o+dM4ggwqI9N5XHLpa3dGJObaNgWW6qBFv/RqXflC8W/u68Q==", null, false, "9cf753c6-1443-4c4a-b773-b44fe54c5443", false, null },
                    { "508d1452-6c15-4266-99db-67cd58e42063", 0, "Martinez", "bba41cc1-5c6d-43af-8898-402ea305fa18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEIVy5UTLPb9gQ4zagBnSaoWN7TdBfwwAKz45qxeVoYAWyZRIpgnzJWaVpOfg2gaekg==", null, false, "139fa8f4-95e0-4687-9986-d0593dcd9c5f", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ddc37f20-ed05-4c33-8851-3231bc2d5461", "283e9c43-e812-4a43-8947-0bc6b66bc8d9" },
                    { "cfafd31e-7726-4462-b80a-3a04ec6dfbd7", "508d1452-6c15-4266-99db-67cd58e42063" }
                });
            */
        }
    }
}
