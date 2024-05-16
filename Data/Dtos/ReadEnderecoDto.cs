using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Data.Dtos;

public class ReadEnderecoDto
{
    public int id { get; set; }
    public string Logradouro { get; set; }
    public int Numero { get; set; }
}
