using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using profefolio.Models.Entities;

namespace profefolio.Models;

public class ApplicationDbContext : IdentityDbContext<Persona>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        Persona user = new Persona()
        {
            Email = "Carlos.Torres123@mail.com",
            NormalizedEmail = "CARLOS.TORRES@123MAIL.COM",
            Apellido = "Torres",
            Nombre = "Carlos",
            Deleted = false,
            Nacimiento = new DateTime(1999, 07, 10).ToUniversalTime()

        };

        var hasher = new PasswordHasher<Persona>();
        user.PasswordHash = hasher.HashPassword(user, "Carlos.Torres123");

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
                user
            );

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(identityUserRole);

    }

     public DbSet<Colegio> Colegios
    {
        get;
        set;
    }
}