using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do cinema é obrigatório.")]
    public string Nome { get; set; }
    // Fazendo relação de entidades 1 para 1 de endereço com o cinema:
    public int EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }
    // Fazendo relação de entidades 1 para muitos com o cinema e a sessão:
    public virtual ICollection<Sessao> Sessoes { get; set; }
}
