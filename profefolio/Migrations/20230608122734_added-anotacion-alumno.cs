using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class addedanotacionalumno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: "eb0772ca-b805-49fb-8ebf-1442977c828a"); */

            migrationBuilder.CreateTable(
                name: "AnotacionesAlumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlumnoId = table.Column<int>(type: "integer", nullable: false),
                    MateriaListaId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnotacionesAlumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnotacionesAlumnos_ClasesAlumnosColegios_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "ClasesAlumnosColegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnotacionesAlumnos_MateriaListas_MateriaListaId",
                        column: x => x.MateriaListaId,
                        principalTable: "MateriaListas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /* migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "040316f5-f4c4-4d9e-bfb6-83876e344531", "b6f4b805-0f17-4b4b-8ee8-2564fc15b3fc", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
                    { "0f1922be-1708-4ca4-a1f6-415998f62784", "5843248b-5368-416c-aebc-2211cbb9c453", "Profesor", "PROFESOR" },
                    { "274d3c8f-165d-45e2-b04b-e212861703c4", "5976e0aa-4cd3-4219-ba77-bfaa127d993b", "Alumno", "ALUMNO" },
                    { "ae8b3745-5481-4827-84d5-d2699cdf5daf", "3db3d4f8-89d2-4dc7-b8b7-d6d81e6cdfdf", "Master", "MASTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "621f29c3-10e4-41d5-8e14-772675e15964", 0, "Torres", "379e1818-6aa2-4c4a-9169-165ccc13a9fc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEKkZfnkOwBH17/JG/QtZwBvOOyQOWfQSNEtPI3S6xSwcKzpEyf5z4JzmKWvy86bECA==", null, false, "cc0438ca-b9d6-42d1-bc11-81b390160c7c", false, null },
                    { "d083749f-0706-4fc5-81b7-0515713ceb53", 0, "Martinez", "205faa27-0752-44bc-8493-ebfcd297ff6a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAEEuUstd5+hk81MCgt44BoFT7Mva92aM6rhI4NdT/kwgFLWJFL0Dw5DpAq/vNADAj1g==", null, false, "8adab154-20ca-4d8a-bcd5-98208d8211ea", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ae8b3745-5481-4827-84d5-d2699cdf5daf", "621f29c3-10e4-41d5-8e14-772675e15964" },
                    { "040316f5-f4c4-4d9e-bfb6-83876e344531", "d083749f-0706-4fc5-81b7-0515713ceb53" }
                }); */

            migrationBuilder.CreateIndex(
                name: "IX_AnotacionesAlumnos_AlumnoId",
                table: "AnotacionesAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnotacionesAlumnos_MateriaListaId",
                table: "AnotacionesAlumnos",
                column: "MateriaListaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotacionesAlumnos");

            /* migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1922be-1708-4ca4-a1f6-415998f62784");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "274d3c8f-165d-45e2-b04b-e212861703c4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ae8b3745-5481-4827-84d5-d2699cdf5daf", "621f29c3-10e4-41d5-8e14-772675e15964" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "040316f5-f4c4-4d9e-bfb6-83876e344531", "d083749f-0706-4fc5-81b7-0515713ceb53" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "040316f5-f4c4-4d9e-bfb6-83876e344531");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae8b3745-5481-4827-84d5-d2699cdf5daf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "621f29c3-10e4-41d5-8e14-772675e15964");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d083749f-0706-4fc5-81b7-0515713ceb53");

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
                }); */
        }
    }
}
