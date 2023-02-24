using Microsoft.EntityFrameworkCore;
using profefolio.Models.Entities;

namespace profefolio.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Persona> Personas
    {
        get;
        set;
    }
}