﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using profefolio.Models;

#nullable disable

namespace profefolio.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1",
                            ConcurrencyStamp = "967898bd-ced3-4cca-8183-18ae8b478315",
                            Name = "Master",
                            NormalizedName = "MASTER"
                        },
                        new
                        {
                            Id = "347c6b56-5d1a-4e37-a526-6a2bf4dfb2c1",
                            ConcurrencyStamp = "ff5643fe-79c7-4ada-9416-68cd783344e5",
                            Name = "Alumno",
                            NormalizedName = "ALUMNO"
                        },
                        new
                        {
                            Id = "27683e2e-3c43-431b-b8ac-02314ebd2d40",
                            ConcurrencyStamp = "8f8fbe31-f1de-488f-89b4-9a50eabec092",
                            Name = "Profesor",
                            NormalizedName = "PROFESOR"
                        },
                        new
                        {
                            Id = "675f43a7-36c9-41d0-9d31-65ee4a40ffe4",
                            ConcurrencyStamp = "e60d57af-daf0-4ce0-b242-0bd50c53ec2e",
                            Name = "Administrador de Colegio",
                            NormalizedName = "ADMINISTRADOR DE COLEGIO"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "511fd355-5b35-48e3-86f3-1ad9e99c1264",
                            RoleId = "3fd5b85c-382a-4435-8e22-c19e5f4c3bc1"
                        },
                        new
                        {
                            UserId = "e4c8c216-d986-40c5-93e3-600f284a7958",
                            RoleId = "675f43a7-36c9-41d0-9d31-65ee4a40ffe4"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("profefolio.Models.Entities.Ciclo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.ToTable("Ciclos");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Clase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Anho")
                        .HasColumnType("integer");

                    b.Property<int>("CicloId")
                        .HasColumnType("integer");

                    b.Property<int>("ColegioId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.HasIndex("ColegioId");

                    b.ToTable("Clases");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Colegio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("PersonaId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("Colegios");
                });

            modelBuilder.Entity("profefolio.Models.Entities.ColegioProfesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ColegioId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PersonaId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ColegioId");

                    b.HasIndex("PersonaId");

                    b.ToTable("ColegiosProfesors");
                });

            modelBuilder.Entity("profefolio.Models.Entities.ColegiosAlumnos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ColegioId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PersonaId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ColegioId");

                    b.HasIndex("PersonaId");

                    b.ToTable("ColegiosAlumnos");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nombre_Materia")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("profefolio.Models.Entities.MateriaLista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClaseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<int>("MateriaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("ProfesorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClaseId");

                    b.HasIndex("MateriaId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("MateriaListas");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Persona", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Direccion")
                        .HasColumnType("text");

                    b.Property<string>("Documento")
                        .HasColumnType("text");

                    b.Property<string>("DocumentoTipo")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("EsM")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("Nacimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "511fd355-5b35-48e3-86f3-1ad9e99c1264",
                            AccessFailedCount = 0,
                            Apellido = "Torres",
                            ConcurrencyStamp = "893ec1c6-1890-4db5-9e92-6db47bd663d8",
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Email = "Carlos.Torres123@mail.com",
                            EmailConfirmed = false,
                            EsM = true,
                            LockoutEnabled = false,
                            Modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nacimiento = new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc),
                            Nombre = "Carlos",
                            NormalizedEmail = "CARLOS.TORRES123@MAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEOkSredtQO/Tq9ub0o9kFSBdv4IyXjyC+JAzzVq9D7+4fBE6Xl5E52ksrKCN7kI7cg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ff2ee1a5-9361-41aa-a75b-0e2f7e4ea55a",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "e4c8c216-d986-40c5-93e3-600f284a7958",
                            AccessFailedCount = 0,
                            Apellido = "Martinez",
                            ConcurrencyStamp = "bea1738d-3956-48df-aa63-93433ed4c9b4",
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Email = "Juan.Martinez123@mail.com",
                            EmailConfirmed = false,
                            EsM = true,
                            LockoutEnabled = false,
                            Modified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nacimiento = new DateTime(1999, 7, 10, 4, 0, 0, 0, DateTimeKind.Utc),
                            Nombre = "Juan",
                            NormalizedEmail = "JUAN.MARTINEZ123@MAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEAILkdPJTmgi/EHDH/ID//TQ54RC2SWmcSUJZiuPoq5g6V1QPFp+VfMz+PJEC++aJw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cc1269f9-4110-4f7c-9a7b-d975348a368c",
                            TwoFactorEnabled = false
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Persona", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Persona", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Persona", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Persona", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("profefolio.Models.Entities.Clase", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Ciclo", "Ciclo")
                        .WithMany()
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Colegio", "Colegio")
                        .WithMany()
                        .HasForeignKey("ColegioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Colegio");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Colegio", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Persona", "personas")
                        .WithMany()
                        .HasForeignKey("PersonaId");

                    b.Navigation("personas");
                });

            modelBuilder.Entity("profefolio.Models.Entities.ColegioProfesor", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Colegio", "Colegio")
                        .WithMany("ColegioProfesores")
                        .HasForeignKey("ColegioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Persona", "Persona")
                        .WithMany("ColegiosProfesor")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colegio");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("profefolio.Models.Entities.ColegiosAlumnos", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Colegio", "Colegio")
                        .WithMany("ColegiosAlumnos")
                        .HasForeignKey("ColegioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Persona", "Persona")
                        .WithMany("ColegiosAlumnos")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colegio");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("profefolio.Models.Entities.MateriaLista", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Clase", "Clase")
                        .WithMany("MateriaListas")
                        .HasForeignKey("ClaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.Persona", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clase");

                    b.Navigation("Materia");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Clase", b =>
                {
                    b.Navigation("MateriaListas");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Colegio", b =>
                {
                    b.Navigation("ColegioProfesores");

                    b.Navigation("ColegiosAlumnos");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Persona", b =>
                {
                    b.Navigation("ColegiosAlumnos");

                    b.Navigation("ColegiosProfesor");
                });
#pragma warning restore 612, 618
        }
    }
}