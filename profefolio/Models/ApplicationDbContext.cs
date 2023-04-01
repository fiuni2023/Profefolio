using Microsoft.EntityFrameworkCore;
using profefolio.Models.Entities;
using profefolio.Models;

namespace profefolio.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = new Persona()
        {
            Email = "Carlos.Torres123@mail.com",
            NormalizedEmail = "CARLOS.TORRES123@MAIL.COM",
            Apellido = "Torres",
            Nombre = "Carlos",
            Deleted = false,
            Nacimiento = new DateTime(1999, 07, 10).ToUniversalTime(),
            EsM = true

        };

        var hasher = new PasswordHasher<Persona>();
        user.PasswordHash = hasher.HashPassword(user, "Carlos.Torres123");

        var administrador = new Persona()
        {
            Email = "Juan.Martinez123@mail.com",
            NormalizedEmail = "JUAN.MARTINEZ123@MAIL.COM",
            Apellido = "Martinez",
            Nombre = "Juan",
            Deleted = false,
            Nacimiento = new DateTime(1999, 07, 10).ToUniversalTime(),
            EsM = true
        };

        administrador.PasswordHash = hasher.HashPassword(administrador, "Juan.Martinez123");

        var rolMaster = new IdentityRole()
        {
            Name = "Master",
            NormalizedName = "MASTER"
        };

        var rolAdministrador = new IdentityRole()
        {
            Name = "Administrador de Colegio",
            NormalizedName = "ADMINISTRADOR DE COLEGIO"
        };

        var rolProfesor = new IdentityRole()
        {
            Name = "Profesor",
            NormalizedName = "PROFESOR"
        };

        var rolAlumno = new IdentityRole()
        {
            Name = "Alumno",
            NormalizedName = "ALUMNO"
        };

        var identityUserRole = new IdentityUserRole<string>()
        {
            UserId = user.Id,
            RoleId = rolMaster.Id
        };

        var administradorRol = new IdentityUserRole<string>()
        {
            UserId = administrador.Id,
            RoleId = rolAdministrador.Id
        };


        modelBuilder.Entity<IdentityRole>()
            .HasData(rolMaster);

        modelBuilder.Entity<IdentityRole>()
            .HasData(rolAlumno);

        modelBuilder.Entity<IdentityRole>()
            .HasData(rolProfesor);

        modelBuilder.Entity<IdentityRole>()
            .HasData(rolAdministrador);

        modelBuilder.Entity<Persona>()
            .HasData(
                user, administrador
            );

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(identityUserRole, administradorRol);

    }


    public DbSet<Materia> Materias { get; set; }
    public DbSet<Colegio> Colegios
    {
        get;
        set;
    }

    public DbSet<Ciclo> Ciclos { get; set; }
    public DbSet<Clase> Clases { get; set; }
    public DbSet<ColegiosAlumnos> ColegiosAlumnos { get; set; }
}