using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class AddModelEventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DeleteData(
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
                */

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MateriaId = table.Column<int>(type: "integer", nullable: false),
                    ClaseId = table.Column<int>(type: "integer", nullable: false),
                    ColegioId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Colegios_ColegioId",
                        column: x => x.ColegioId,
                        principalTable: "Colegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          /*  migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fa02bda-cdb5-4509-bfce-65819be02a98", "2d582d09-a62e-4399-93b4-60bb692c3a43", "Alumno", "ALUMNO" },
                    { "45397c94-5cb8-49d5-8139-cb0728996890", "1d2c1c9c-d706-4a57-bf35-15169a69cba9", "Master", "MASTER" },
                    { "4bffb140-dc9b-47cf-a236-37903b11feeb", "3df98399-543b-4b1c-8cfe-ffa23150a935", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "eb69f976-2197-4894-80e2-1c5d74a96191", "dd0a2920-298b-4381-a7a3-f14deb35a33c", "Profesor", "PROFESOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8038e938-8c39-4551-b2a6-cba6c05dfc11", 0, "Martinez", "83036624-a8ad-404c-80ea-575c728b39a4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAECFVIGDpdHexDYQZ3kLikKW0D5kdUiordmnRcy7Eyupu8IFS+Icc7D65hF4IckyWkw==", null, false, "86d23815-b589-461e-9cbf-49e3f2f5c971", false, null },
                    { "8dc4a09c-18d3-4d44-839e-c2e239d97b71", 0, "Torres", "df38283d-1622-43a4-b353-f87736a45d7c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEBW0FQ6b0zOS9uYww5kM30wv4+p1Do2ugpyevUZyWyxbhlVVTV02ilIa+6UbsWtXTg==", null, false, "1b6f2b59-a46b-43f6-84eb-cf2af7ee62b3", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4bffb140-dc9b-47cf-a236-37903b11feeb", "8038e938-8c39-4551-b2a6-cba6c05dfc11" },
                    { "45397c94-5cb8-49d5-8139-cb0728996890", "8dc4a09c-18d3-4d44-839e-c2e239d97b71" }
                });
            */
            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ClaseId",
                table: "Eventos",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ColegioId",
                table: "Eventos",
                column: "ColegioId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_MateriaId",
                table: "Eventos",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");

           /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fa02bda-cdb5-4509-bfce-65819be02a98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb69f976-2197-4894-80e2-1c5d74a96191");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4bffb140-dc9b-47cf-a236-37903b11feeb", "8038e938-8c39-4551-b2a6-cba6c05dfc11" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45397c94-5cb8-49d5-8139-cb0728996890", "8dc4a09c-18d3-4d44-839e-c2e239d97b71" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45397c94-5cb8-49d5-8139-cb0728996890");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bffb140-dc9b-47cf-a236-37903b11feeb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8038e938-8c39-4551-b2a6-cba6c05dfc11");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8dc4a09c-18d3-4d44-839e-c2e239d97b71");

            migrationBuilder.InsertData(
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
            */
        }
    }
}
