using Microsoft.EntityFrameworkCore;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Estabelecendo as chaves que a sessão terá { id do filme, id do cinema }.
        modelBuilder.Entity<Sessao>().HasKey(sessao => new { sessao.FilmeId, sessao.CinemaId });
        // Estabelecendo o relacionamento entre as entidades sessão e cinema:
        modelBuilder.Entity<Sessao>().HasOne(sessao => sessao.Cinema)
                                     .WithMany(cinema => cinema.Sessoes)
                                     .HasForeignKey(sessao => sessao.CinemaId);
        // Estabelecendo o relacionamento entre as entidades sessão e filme:
        modelBuilder.Entity<Sessao>().HasOne(sessao => sessao.Filme)
                                     .WithMany(filme => filme.Sessoes)
                                     .HasForeignKey(sessao => sessao.FilmeId);
        // Definindo o tipo de deleção, retirando o padrão que é em "cascata"(deletando tudo que estiver ligado a ele).
        // Alterando para o tipo "restrict", que rejeita a deleção caso haja dependencias entre as entidades.
        modelBuilder.Entity<Endereco>().HasOne(endereco => endereco.Cinema)
                                       .WithOne(cinema => cinema.Endereco)
                                       .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
}
