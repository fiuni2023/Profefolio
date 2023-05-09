using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddRelacionHorasCatedrasMaterias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "345d7eef-591f-44a4-bc6d-d5ba8ad37007");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdafa56c-bbff-4f2b-99a7-d531bd31edde");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "de91e436-c9eb-487e-8149-e61209c326a3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "e9fa2d90-d576-4e8b-9160-b78610e7414c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21d4fef8-d68c-4b85-92e0-c1e44613e6a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "de91e436-c9eb-487e-8149-e61209c326a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9fa2d90-d576-4e8b-9160-b78610e7414c");
            */
            migrationBuilder.CreateTable(
                name: "HorasCatedrasMaterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraCatedraId = table.Column<int>(type: "integer", nullable: false),
                    MateriaListaId = table.Column<int>(type: "integer", nullable: false),
                    Dia = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasCatedrasMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorasCatedrasMaterias_HorasCatedras_HoraCatedraId",
                        column: x => x.HoraCatedraId,
                        principalTable: "HorasCatedras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorasCatedrasMaterias_MateriaListas_MateriaListaId",
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
                    { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "b1c31710-2896-4516-84b6-9457fbb00ac4", "Master", "MASTER" },
                    { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "3e77738d-b274-4987-9d0e-b049e2efe0d4", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "7cb8c4c8-dd0b-41b1-8411-386c00126848", "1990aaab-0732-46bc-b121-d813d6e1434d", "Alumno", "ALUMNO" },
                    { "e17270b4-4e5d-4c61-b33c-c5845652223f", "36b805ca-736e-4b7d-8f89-5f7de26525eb", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3bc82905-089c-4501-9bf8-80ac7ae2ded3", 0, "Torres", "8379a705-3049-4217-8fc3-b15262d57172", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEK75pSDQ/V91u2/yRr71hZED5dfg6qnYeuc3Z5wU95eHRE66hD9U8zlUCUg2FxgNhg==", null, false, "27804b21-5b81-4c3e-af9b-4b6addcd8c0f", false, null },
                    { "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f", 0, "Martinez", "364a0b7f-e403-4c48-81e6-425bc1ccc4e4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEICvEyqsGD5mG5E3du+i1lGvTjxucPedmuCGduy9J7rKUVzAhaPgywTr5AaLWwqGgA==", null, false, "ec28fb68-8fb3-40c1-959a-1592a7f40383", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "3bc82905-089c-4501-9bf8-80ac7ae2ded3" },
                    { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorasCatedrasMaterias_HoraCatedraId",
                table: "HorasCatedrasMaterias",
                column: "HoraCatedraId");

            migrationBuilder.CreateIndex(
                name: "IX_HorasCatedrasMaterias_MateriaListaId",
                table: "HorasCatedrasMaterias",
                column: "MateriaListaId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorasCatedrasMaterias");

            /*migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cb8c4c8-dd0b-41b1-8411-386c00126848");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e17270b4-4e5d-4c61-b33c-c5845652223f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42d34633-dd40-4337-8e8a-0f3c50c8afcd", "3bc82905-089c-4501-9bf8-80ac7ae2ded3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5becbfc3-23e1-4a3c-8eef-1f7287bc168d", "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42d34633-dd40-4337-8e8a-0f3c50c8afcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5becbfc3-23e1-4a3c-8eef-1f7287bc168d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bc82905-089c-4501-9bf8-80ac7ae2ded3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d59ddf6-21ff-4d87-bc44-d6fc1b4ded7f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "9319a521-f625-4f5c-847c-b5f59f5fe7c4", "Master", "MASTER" },
                    { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "480d6cde-e30f-42f8-9b92-aea07e3b6420", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "345d7eef-591f-44a4-bc6d-d5ba8ad37007", "11b3534b-c6f8-41bd-bbbf-dcc149eb8be7", "Alumno", "ALUMNO" },
                    { "cdafa56c-bbff-4f2b-99a7-d531bd31edde", "6bfb0fa4-879c-419f-9b96-8d25e8c93c1f", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "de91e436-c9eb-487e-8149-e61209c326a3", 0, "Torres", "1811c8c5-4452-4956-aaa2-4902bb59b47f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAECnMeda8ocMej2xdIu+5yPJVxtJQFXDUcWmkSkLBG+EbqWT7d88XyQlQDl6oBQaiEA==", null, false, "8cd0710f-656a-432e-be1a-f806d99652e6", false, null },
                    { "e9fa2d90-d576-4e8b-9160-b78610e7414c", 0, "Martinez", "a29f0bf7-55be-4bd5-b3f3-c73776cde3c7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEDvNsJ6SchQoCcg31Xap/J4LvVHAbj6Jzzx3kr0uDvIQ63030OI5907sF0XlqMqGvQ==", null, false, "368be0a7-3377-49dd-8cd4-58fe875bbb4b", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "21d4fef8-d68c-4b85-92e0-c1e44613e6a6", "de91e436-c9eb-487e-8149-e61209c326a3" },
                    { "2c8c3c7a-7ac4-4e51-8f71-7b76e5aa4854", "e9fa2d90-d576-4e8b-9160-b78610e7414c" }
                });*/
        }
    }
}
