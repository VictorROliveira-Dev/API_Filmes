using Microsoft.EntityFrameworkCore;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options) { }

    public DbSet<Filme> Filmes { get; set; }
}
