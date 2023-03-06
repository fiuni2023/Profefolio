using Microsoft.EntityFrameworkCore;
using profefolio.Models.Entities;
namespace profefolio.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Persona> Personas
    {
        get;
        set;
    }

     public DbSet<Colegio> Colegios
    {
        get;
        set;
    }
}