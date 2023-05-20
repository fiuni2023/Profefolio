using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace profefolio.Migrations
{
    public partial class identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Apellido = table.Column<string>(type: "text", nullable: true),
                    Nacimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: true),
                    DocumentoTipo = table.Column<string>(type: "text", nullable: true),
                    EsM = table.Column<bool>(type: "boolean", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            // migrationBuilder.CreateTable(
            //     name: "Ciclos",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         Nombre = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Ciclos", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Materias",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         Nombre_Materia = table.Column<string>(type: "text", nullable: true),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Materias", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetRoleClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         RoleId = table.Column<string>(type: "text", nullable: false),
            //         ClaimType = table.Column<string>(type: "text", nullable: true),
            //         ClaimValue = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         UserId = table.Column<string>(type: "text", nullable: false),
            //         ClaimType = table.Column<string>(type: "text", nullable: true),
            //         ClaimValue = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserLogins",
            //     columns: table => new
            //     {
            //         LoginProvider = table.Column<string>(type: "text", nullable: false),
            //         ProviderKey = table.Column<string>(type: "text", nullable: false),
            //         ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
            //         UserId = table.Column<string>(type: "text", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserRoles",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "text", nullable: false),
            //         RoleId = table.Column<string>(type: "text", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "AspNetUserTokens",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "text", nullable: false),
            //         LoginProvider = table.Column<string>(type: "text", nullable: false),
            //         Name = table.Column<string>(type: "text", nullable: false),
            //         Value = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Colegios",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         Nombre = table.Column<string>(type: "text", nullable: true),
            //         PersonaId = table.Column<string>(type: "text", nullable: true),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Colegios", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Colegios_AspNetUsers_PersonaId",
            //             column: x => x.PersonaId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id");
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Clases",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         ColegioId = table.Column<int>(type: "integer", nullable: false),
            //         CicloId = table.Column<int>(type: "integer", nullable: false),
            //         Nombre = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
            //         Turno = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
            //         Anho = table.Column<int>(type: "integer", nullable: false),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
                // constraints: table =>
                // {
                //     table.PrimaryKey("PK_Clases", x => x.Id);
                //     table.ForeignKey(
                //         name: "FK_Clases_Ciclos_CicloId",
                //         column: x => x.CicloId,
                //         principalTable: "Ciclos",
                //         principalColumn: "Id",
                //         onDelete: ReferentialAction.Cascade);
                //     table.ForeignKey(
                //         name: "FK_Clases_Colegios_ColegioId",
                //         column: x => x.ColegioId,
                //         principalTable: "Colegios",
                //         principalColumn: "Id",
                //         onDelete: ReferentialAction.Cascade);
                // });

            // migrationBuilder.CreateTable(
            //     name: "ColegiosAlumnos",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         ColegioId = table.Column<int>(type: "integer", nullable: false),
            //         PersonaId = table.Column<string>(type: "text", nullable: false),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ColegiosAlumnos", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_ColegiosAlumnos_AspNetUsers_PersonaId",
            //             column: x => x.PersonaId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_ColegiosAlumnos_Colegios_ColegioId",
            //             column: x => x.ColegioId,
            //             principalTable: "Colegios",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "ColegiosProfesors",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "integer", nullable: false)
            //             .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //         ColegioId = table.Column<int>(type: "integer", nullable: false),
            //         PersonaId = table.Column<string>(type: "text", nullable: false),
            //         Deleted = table.Column<bool>(type: "boolean", nullable: false),
            //         Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         CreatedBy = table.Column<string>(type: "text", nullable: true),
            //         Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            //         ModifiedBy = table.Column<string>(type: "text", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ColegiosProfesors", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_ColegiosProfesors_AspNetUsers_PersonaId",
            //             column: x => x.PersonaId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_ColegiosProfesors_Colegios_ColegioId",
            //             column: x => x.ColegioId,
            //             principalTable: "Colegios",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            migrationBuilder.CreateTable(
                name: "MateriaLista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfesorId = table.Column<int>(type: "integer", nullable: false),
                    ClaseId = table.Column<int>(type: "integer", nullable: false),
                    MateriaId = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaLista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaLista_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

        //     migrationBuilder.InsertData(
        //         table: "AspNetRoles",
        //         columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
        //         values: new object[,]
        //         {
        //             { "27df30ef-7db0-4e2f-9cc1-802159a49306", "aa986282-06e5-47ec-a00d-3420e123f0aa", "Administrador de Colegio", "ADMINISTRADOR DE COLEGIO" },
        //             { "63cd5cb2-fe1e-431c-a16d-400367961c4b", "842cdb42-e43a-41c8-95f6-c77e34123e62", "Alumno", "ALUMNO" },
        //             { "c1f74249-35f0-4666-b196-76e1cebd9872", "15b40514-fa74-4001-bd96-fc1c725f7f02", "Profesor", "PROFESOR" },
        //             { "e2ee620b-23d1-47b0-b116-4cb1a3e77896", "47deffce-466c-4788-a4b4-6f6e6e8e4040", "Master", "MASTER" }
        //         });

        //     migrationBuilder.InsertData(
        //         table: "AspNetUsers",
        //         columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Created", "CreatedBy", "Deleted", "Direccion", "Documento", "DocumentoTipo", "Email", "EmailConfirmed", "EsM", "LockoutEnabled", "LockoutEnd", "Modified", "ModifiedBy", "Nacimiento", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
        //         values: new object[,]
        //         {
        //             { "e7fcfdba-8419-4239-92a3-f425aa62de49", 0, "Martinez", "db46308c-fb6b-420f-ad63-854332dbcc04", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Juan.Martinez123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Juan", "JUAN.MARTINEZ123@MAIL.COM", null, "AQAAAAEAACcQAAAAELZh9zdQwhgWclr1xNC5vWHWtH1mOYVAg1QuaHHld1gAjeh6MEPGDMH6v7RKGF63sQ==", null, false, "db6c291c-e079-48a2-bbbd-0d2b581f032a", false, null },
        //             { "edb72cd3-6181-4aae-9568-b69c60ce1796", 0, "Torres", "d9bb947e-7a38-4fac-b1c1-570fd1cdb7e5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "Carlos.Torres123@mail.com", false, true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc), "Carlos", "CARLOS.TORRES123@MAIL.COM", null, "AQAAAAEAACcQAAAAEOVF8cEkB1XggIlVfx4YG/hgHC29wKELjKhMYDS3dQmICjaH1LmzICSk9WG2DMHpAA==", null, false, "df5b278e-7490-4541-9305-fad1fc843c2c", false, null }
        //         });

        //     migrationBuilder.InsertData(
        //         table: "AspNetUserRoles",
        //         columns: new[] { "RoleId", "UserId" },
        //         values: new object[,]
        //         {
        //             { "27df30ef-7db0-4e2f-9cc1-802159a49306", "e7fcfdba-8419-4239-92a3-f425aa62de49" },
        //             { "e2ee620b-23d1-47b0-b116-4cb1a3e77896", "edb72cd3-6181-4aae-9568-b69c60ce1796" }
        //         });

        //     migrationBuilder.CreateIndex(
        //         name: "IX_AspNetRoleClaims_RoleId",
        //         table: "AspNetRoleClaims",
        //         column: "RoleId");

        //     migrationBuilder.CreateIndex(
        //         name: "RoleNameIndex",
        //         table: "AspNetRoles",
        //         column: "NormalizedName",
        //         unique: true);

        //     migrationBuilder.CreateIndex(
        //         name: "IX_AspNetUserClaims_UserId",
        //         table: "AspNetUserClaims",
        //         column: "UserId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_AspNetUserLogins_UserId",
        //         table: "AspNetUserLogins",
        //         column: "UserId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_AspNetUserRoles_RoleId",
        //         table: "AspNetUserRoles",
        //         column: "RoleId");

        //     migrationBuilder.CreateIndex(
        //         name: "EmailIndex",
        //         table: "AspNetUsers",
        //         column: "NormalizedEmail");

        //     migrationBuilder.CreateIndex(
        //         name: "UserNameIndex",
        //         table: "AspNetUsers",
        //         column: "NormalizedUserName",
        //         unique: true);

        //     migrationBuilder.CreateIndex(
        //         name: "IX_Clases_CicloId",
        //         table: "Clases",
        //         column: "CicloId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_Clases_ColegioId",
        //         table: "Clases",
        //         column: "ColegioId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_Colegios_PersonaId",
        //         table: "Colegios",
        //         column: "PersonaId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_ColegiosAlumnos_ColegioId",
        //         table: "ColegiosAlumnos",
        //         column: "ColegioId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_ColegiosAlumnos_PersonaId",
        //         table: "ColegiosAlumnos",
        //         column: "PersonaId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_ColegiosProfesors_ColegioId",
        //         table: "ColegiosProfesors",
        //         column: "ColegioId");

        //     migrationBuilder.CreateIndex(
        //         name: "IX_ColegiosProfesors_PersonaId",
        //         table: "ColegiosProfesors",
        //         column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaLista_ClaseId",
                table: "MateriaLista",
                column: "ClaseId");
         }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //     migrationBuilder.DropTable(
        //         name: "AspNetRoleClaims");

        //     migrationBuilder.DropTable(
        //         name: "AspNetUserClaims");

        //     migrationBuilder.DropTable(
        //         name: "AspNetUserLogins");

        //     migrationBuilder.DropTable(
        //         name: "AspNetUserRoles");

        //     migrationBuilder.DropTable(
        //         name: "AspNetUserTokens");

        //     migrationBuilder.DropTable(
        //         name: "ColegiosAlumnos");

        //     migrationBuilder.DropTable(
        //         name: "ColegiosProfesors");

        //     migrationBuilder.DropTable(
        //         name: "MateriaLista");

        //     migrationBuilder.DropTable(
        //         name: "Materias");

        //     migrationBuilder.DropTable(
        //         name: "AspNetRoles");

        //     migrationBuilder.DropTable(
        //         name: "Clases");

        //     migrationBuilder.DropTable(
        //         name: "Ciclos");

        //     migrationBuilder.DropTable(
        //         name: "Colegios");

        //     migrationBuilder.DropTable(
        //         name: "AspNetUsers");
        }
    }
}
