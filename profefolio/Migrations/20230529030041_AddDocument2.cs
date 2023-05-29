using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddDocument2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
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

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6933935c-2222-4be4-aded-0f55e9998fea", "35e590fd-0c5c-4e4b-b30d-634be1c0ff07", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "74ef6dda-d609-419d-af89-ccf103c24e20", "5b3aedc7-f73a-4222-a390-5a69bf9852a6", "Profesor", "PROFESOR" },
                    { "9b1cfb7e-7e96-433b-9f41-a0cbdddcb203", "9755bf1c-c00f-4c55-a882-55b9aa24b496", "Master", "MASTER" },
                    { "fbfb9bf4-980f-4718-920e-6106abc03fa1", "2cafde05-1613-4d5c-984a-1d45ef456df6", "Alumno", "ALUMNO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "10e59d44-82da-4b19-abf6-cae82993e06f", 0, "Torres", "29f73ebd-67f4-45ae-8ae0-60adc7371f44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEM5P7GenU6WSapwPBgZ4yeiggqhnFybuY/8GtycaLeYyZD9LDi/qgBeE7v9agiUEYg==", null, false, "f6f62b1f-3dd7-4fa1-ac88-0579cf3a6cfa", false, null },
                    { "b06bea50-e1c0-49a4-85ce-bb923692e597", 0, "Martinez", "cf356930-e062-4303-9257-7e9d87598da7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAENhKOn7+tp0XirzR+/rS2DPuZM5NSVwZeT1RNNjLTYD/UXv6mdy0yk7ZLqPD76lxLA==", null, false, "89445889-5864-49c7-90f5-634fffe23c22", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9b1cfb7e-7e96-433b-9f41-a0cbdddcb203", "10e59d44-82da-4b19-abf6-cae82993e06f" },
                    { "6933935c-2222-4be4-aded-0f55e9998fea", "b06bea50-e1c0-49a4-85ce-bb923692e597" }
                });
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74ef6dda-d609-419d-af89-ccf103c24e20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbfb9bf4-980f-4718-920e-6106abc03fa1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b1cfb7e-7e96-433b-9f41-a0cbdddcb203", "10e59d44-82da-4b19-abf6-cae82993e06f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6933935c-2222-4be4-aded-0f55e9998fea", "b06bea50-e1c0-49a4-85ce-bb923692e597" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6933935c-2222-4be4-aded-0f55e9998fea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b1cfb7e-7e96-433b-9f41-a0cbdddcb203");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10e59d44-82da-4b19-abf6-cae82993e06f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b06bea50-e1c0-49a4-85ce-bb923692e597");

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

            migrationBuilder.AddColumn<string>(
                name: "ProfesorId",
                table: "Documentos",
                type: "text",
                nullable: true);

            /*migrationBuilder.InsertData(
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
