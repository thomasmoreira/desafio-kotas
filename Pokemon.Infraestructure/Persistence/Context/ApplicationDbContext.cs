using Microsoft.EntityFrameworkCore;
using Pokemon.Application.DTOs;
using Pokemon.Domain.Entities;

namespace Pokemon.Infraestructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<PokemonMaster> PokemonMasters { get; set; }
    public DbSet<PokemonCapture> PokemonCaptures { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações adicionais se necessário
        base.OnModelCreating(modelBuilder);
    }
}
