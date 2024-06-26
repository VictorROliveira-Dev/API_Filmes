﻿using System.ComponentModel.DataAnnotations;

namespace SistemaAPIFilmes.Data.Dtos;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "O nome do cinema é obrigatório.")]
    public string Nome { get; set; }
}
