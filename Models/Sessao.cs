using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Models;

public class Sessao
{
    // Criando relãção de 1 para muitas entre as entidadades filme e sessão
    public int? FilmeId { get; set; }
    public virtual Filme Filme { get; set; }
    // Fazendo relação de entidades 1 para muitos com o cinema e a sessão:
    public int? CinemaId { get; set; }
    public virtual Cinema Cinema { get; set; }
}
