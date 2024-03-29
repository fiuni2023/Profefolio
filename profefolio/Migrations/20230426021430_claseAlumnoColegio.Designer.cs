﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using profefolio.Models;

#nullable disable

namespace profefolio.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230426021430_claseAlumnoColegio")]
    partial class claseAlumnoColegio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = "2ac32543-9f31-49b2-b2ae-c9ff71457067",
                            ConcurrencyStamp = "3474080e-03d8-4a82-89a7-7db57ba42ab1",
                            Name = "Master",
                            NormalizedName = "MASTER"
                        },
                        new
                        {
                            Id = "0ad0ff24-56a6-4475-9353-39843e489e98",
                            ConcurrencyStamp = "748cf853-cf7e-4f59-beb4-048e274efbc4",
                            Name = "Alumno",
                            NormalizedName = "ALUMNO"
                        },
                        new
                        {
                            Id = "9fff4624-1ef6-4cd9-a17a-ce667c6d2b33",
                            ConcurrencyStamp = "d7e3f724-b756-46dd-a141-4f9ca9dcefca",
                            Name = "Profesor",
                            NormalizedName = "PROFESOR"
                        },
                        new
                        {
                            Id = "c798f617-3e82-4694-9227-951d31ebd0d3",
                            ConcurrencyStamp = "9fcd04a6-54ef-46dd-a680-d379a2fb072f",
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
                            UserId = "95529008-dd05-40b0-8f70-a52bd9f0bb61",
                            RoleId = "2ac32543-9f31-49b2-b2ae-c9ff71457067"
                        },
                        new
                        {
                            UserId = "4142e823-de82-4870-a6e8-719d7c486d68",
                            RoleId = "c798f617-3e82-4694-9227-951d31ebd0d3"
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

            modelBuilder.Entity("profefolio.Models.Entities.ClasesAlumnosColegio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClaseId")
                        .HasColumnType("integer");

                    b.Property<int>("ColegiosAlumnosId")
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

                    b.HasKey("Id");

                    b.HasIndex("ClaseId");

                    b.HasIndex("ColegiosAlumnosId");

                    b.ToTable("ClasesAlumnosColegios");
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

                    b.HasIndex("PersonaId")
                        .IsUnique();

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
                            Id = "95529008-dd05-40b0-8f70-a52bd9f0bb61",
                            AccessFailedCount = 0,
                            Apellido = "Torres",
                            ConcurrencyStamp = "1864725d-fb4b-4ba0-afcd-c796f89098a0",
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
                            PasswordHash = "AQAAAAEAACcQAAAAEBzrh/Q2P8Z2Te/skAH42NEBh9r/YbRMzl8nABGs+ZRcyYyGc7yrPyCASjyxU7pLfA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "db943655-ba49-4645-93b9-e9210675ca27",
                            TwoFactorEnabled = false
                        },
                        new
                        {
                            Id = "4142e823-de82-4870-a6e8-719d7c486d68",
                            AccessFailedCount = 0,
                            Apellido = "Martinez",
                            ConcurrencyStamp = "0a403f15-180f-4784-b061-25304acbb16d",
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
                            PasswordHash = "AQAAAAEAACcQAAAAEBm/J3+ek3NRQ/vxNavHgm39iTWb4aEQxIuMD9RMAsiVgaeNNS8y6bswOTSkqlREeQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "452758b9-1715-4f49-ba03-eec058067855",
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

            modelBuilder.Entity("profefolio.Models.Entities.ClasesAlumnosColegio", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Clase", "Clase")
                        .WithMany("ClasesAlumnosColegios")
                        .HasForeignKey("ClaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("profefolio.Models.Entities.ColegiosAlumnos", "ColegiosAlumnos")
                        .WithMany("ClasesAlumnosColegios")
                        .HasForeignKey("ColegiosAlumnosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clase");

                    b.Navigation("ColegiosAlumnos");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Colegio", b =>
                {
                    b.HasOne("profefolio.Models.Entities.Persona", "personas")
                        .WithOne("Colegio")
                        .HasForeignKey("profefolio.Models.Entities.Colegio", "PersonaId");

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
                        .WithMany("MateriaListas")
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
                    b.Navigation("ClasesAlumnosColegios");

                    b.Navigation("MateriaListas");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Colegio", b =>
                {
                    b.Navigation("ColegioProfesores");

                    b.Navigation("ColegiosAlumnos");
                });

            modelBuilder.Entity("profefolio.Models.Entities.ColegiosAlumnos", b =>
                {
                    b.Navigation("ClasesAlumnosColegios");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Materia", b =>
                {
                    b.Navigation("MateriaListas");
                });

            modelBuilder.Entity("profefolio.Models.Entities.Persona", b =>
                {
                    b.Navigation("Colegio")
                        .IsRequired();

                    b.Navigation("ColegiosAlumnos");

                    b.Navigation("ColegiosProfesor");
                });
#pragma warning restore 612, 618
        }
    }
}
