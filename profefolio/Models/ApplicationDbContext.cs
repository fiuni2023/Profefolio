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



        // definicio de primary key de la tabla ColegiosProfesores
        modelBuilder.Entity<ColegioProfesor>().HasKey(cp => new { cp.Id });

        //definimos el foreign key de Colegio
        modelBuilder.Entity<ColegioProfesor>()
        .HasOne<Colegio>(cp => cp.Colegio)
        .WithMany(c => c.ColegioProfesores)
        .HasForeignKey(c => c.ColegioId);



        //definimos el foreign key de Persona (Profesor)
        modelBuilder.Entity<ColegioProfesor>()
        .HasOne<Persona>(p => p.Persona)
        .WithMany(p => p.ColegiosProfesor)
        .HasForeignKey(p => p.PersonaId);
        
        
        // definicio de primary key de la tabla ColegiosAlumnos
        modelBuilder.Entity<ColegiosAlumnos>().HasKey(ca => new { ca.Id });

        //definimos el foreign key de Colegio
        modelBuilder.Entity<ColegiosAlumnos>()
        .HasOne<Colegio>(ca => ca.Colegio)
        .WithMany(c => c.ColegiosAlumnos)
        .HasForeignKey(c => c.ColegioId);

        //definimos el foreign key de Persona (Alumno)
        modelBuilder.Entity<ColegiosAlumnos>()
        .HasOne<Persona>(p => p.Persona)
        .WithMany(p => p.ColegiosAlumnos)
        .HasForeignKey(p => p.PersonaId);

        //Definimos la relacion entre Clase y Listas de Materias
        modelBuilder.Entity<Clase>()
            .HasMany(c => c.MateriaListas)
            .WithOne(c => c.Clase)
            .HasForeignKey(c => c.ClaseId)
            .HasPrincipalKey(c => c.Id);


        
        /* Relacion muchos a muchos entre clases y alumnoscolegios */

        // definicio de primary key de la tabla ClaseAlumnosColegio
        modelBuilder.Entity<ClasesAlumnosColegio>().HasKey(ca => new { ca.Id });

        //definimos el foreign key de Clase
        modelBuilder.Entity<ClasesAlumnosColegio>()
        .HasOne<Clase>(ca => ca.Clase)
        .WithMany(c => c.ClasesAlumnosColegios)
        .HasForeignKey(c => c.ClaseId);

        //definimos el foreign key de ColegiosAlumnos
        modelBuilder.Entity<ClasesAlumnosColegio>()
        .HasOne<ColegiosAlumnos>(p => p.ColegiosAlumnos)
        .WithMany(p => p.ClasesAlumnosColegios)
        .HasForeignKey(p => p.ColegiosAlumnosId);





        /* Relacion muchos a muchos entre HoraCatedra y MateriaLista */

        // definicio de primary key de la tabla HorasCatedrasMaterias
        modelBuilder.Entity<HorasCatedrasMaterias>().HasKey(ca => new { ca.Id });

        //definimos el foreign key de HoraCatedra
        modelBuilder.Entity<HorasCatedrasMaterias>()
        .HasOne<HoraCatedra>(ca => ca.HoraCatedra)
        .WithMany(c => c.HorariosMaterias)
        .HasForeignKey(c => c.HoraCatedraId);

        //definimos el foreign key de MateriaLista
        modelBuilder.Entity<HorasCatedrasMaterias>()
        .HasOne<MateriaLista>(p => p.MateriaLista)
        .WithMany(p => p.Horarios)
        .HasForeignKey(p => p.MateriaListaId);
    }


    public DbSet<Materia> Materias { get; set; }
    public DbSet<Colegio> Colegios{ get;set; }
    public DbSet<Ciclo> Ciclos { get; set; }
    public DbSet<Clase> Clases { get; set; }
    public DbSet<ColegioProfesor> ColegiosProfesors { get; set; }
    public DbSet<ColegiosAlumnos> ColegiosAlumnos { get; set; }
    public DbSet<MateriaLista> MateriaListas { get; set; }
    public DbSet<ClasesAlumnosColegio> ClasesAlumnosColegios { get; set; }
    public DbSet<HoraCatedra> HorasCatedras { get; set; }
    public DbSet<HorasCatedrasMaterias> HorasCatedrasMaterias { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Rubrica> Rubricas { get; set; }
}