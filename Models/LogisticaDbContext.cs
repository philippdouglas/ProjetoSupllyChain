using Microsoft.EntityFrameworkCore;

public class LogisticaDbContext : DbContext
{
    public LogisticaDbContext(DbContextOptions<LogisticaDbContext> options) : base(options) { }

    public DbSet<Mercadoria> Mercadorias { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Saida> Saidas { get; set; }
}

