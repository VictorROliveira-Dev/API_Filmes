using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Data.Dtos;

public class CreateFilmeDto
{
    [Required(ErrorMessage = "O título do filme é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do filme não pode ser maior que 100 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Gênero do filme é obrigatório.")]
    // Ao invés de usar "maxlength", usamos o "stringlength", para não haver contato direto com o banco.
    [StringLength(50, ErrorMessage = "O gênero não pode ser maior que 50 caracteres.")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatório.")]
    [Range(70, 230, ErrorMessage = "A duração deve ser entre 70 e 230 minutos")]
    public int Duracao { get; set; }
}
