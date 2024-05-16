using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título do filme é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome do filme não pode ser maior que 100 caracteres.")]
    public string Titulo { get; set; }
    
    [Required(ErrorMessage = "O Gênero do filme é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O gênero não pode ser maior que 50 caracteres.")]
    public string Genero { get; set; }
    
    [Required(ErrorMessage = "A duração do filme é obrigatório.")]
    [Range(70, 230, ErrorMessage = "A duração deve ser entre 70 e 230 minutos")]
    public int Duracao { get; set; }
    //Criando relãção de 1 para muitas entre as entidadades filme e sessão
    public virtual ICollection<Sessao> Sessoes { get; set; }
}