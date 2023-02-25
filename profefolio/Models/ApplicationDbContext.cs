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

    public DbSet<Persona> Personas
    {
        get;
        set;
    }
}