using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Configurations;

namespace Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeamento das entidades
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
