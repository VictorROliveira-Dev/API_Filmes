using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAPIFilmes.Data;
using SistemaAPIFilmes.Data.Dtos;
using SistemaAPIFilmes.Models;

namespace SistemaAPIFilmes.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private readonly FilmeContext _filmeContext;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _filmeContext = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso ocorra algum erro na hora da validação</response>
    /// <response code="404">O servidor não conseguiu obter uma resposta no caminho passado.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _filmeContext.Filmes.Add(filme);
        _filmeContext.SaveChanges();

        return CreatedAtAction(nameof(BuscaFilmePorId), new { filmeId = filme.Id }, filme);
    }

    /// <summary>
    /// Busca os filmes no banco de dados
    /// </summary>
    /// <param name="skip">A partir de qual item quer começar?</param>
    /// <param name="take">Quantos filmes quer exibir?</param>
    /// <param name="nomeCinema">Qual filme quer buscar?</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso os filmes sejam recuperados com sucesso</response>
    /// <response code="404">O servidor não conseguiu obter uma resposta no caminho passado.</response>
    // Caso os valores de skip e take não sejam passados, assumem os valores padrão estabelecidos nos parâmetros.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<ReadFilmeDto> BuscaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 5, [FromQuery] string? nomeCinema = null)
    {
        if (nomeCinema == null)
        {
            // Definindo 0 valores padrões de 0 e 5 para a URL, caso o usuário não passe nenhum valor.
            // Paginação de itens da API (skip indica quantos itens quer pular) e (take quantos itens quer pegar):
            return _mapper.Map<List<ReadFilmeDto>>(_filmeContext.Filmes.Skip(skip).Take(take).ToList());
        }
        return _mapper.Map<List<ReadFilmeDto>>(_filmeContext.Filmes.Skip(skip).Take(take).Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema))
                                                                                                                      .ToList());
    }

    /// <summary>
    /// Busca o filme com o ID especificado, no banco de dados
    /// </summary>
    /// <param name="filmeId">Campo ID necessário para a captura de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o filme seja recuperado com sucesso</response>
    /// <response code="404">O servidor não conseguiu obter uma resposta no caminho passado.</response>
    [HttpGet("{filmeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult BuscaFilmePorId(int filmeId)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(f => f.Id == filmeId);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza um filme no banco de dados
    /// </summary>
    /// <param name="id">Campo ID necessário para a captura de um filme, para poder atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja atualizado com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        // Mapeando o filme com os novos dados do filmeDto, para atualizar os valores.
        // O método "Map" dessa forma, serve para atualizar os valores do filme.
        _mapper.Map(filmeDto, filme);
        _filmeContext.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme no banco de dados
    /// </summary>
    /// <param name="id">Campo ID necessário para a captura de um filme, para poder atualizar</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja atualizado com sucesso</response>
    /// <response code="400">Caso ocorra algum erro na hora da validação</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarFilmeParcialmente(int id, JsonPatchDocument<UpdateFilmeDto> filmePatch)
    {
        // Buscando por filmes:
        var filme = _filmeContext.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        // Utilizando Newtonsoft.Json para fazer as alterações parcias:
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        filmePatch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        // Remapeando o filme com as alterações:
        _mapper.Map(filmeParaAtualizar, filme);
        _filmeContext.SaveChanges();
        // Retornando status de resposta:
        return NoContent();
    }

    /// <summary>
    /// Apaga um filme no banco de dados
    /// </summary>
    /// <param name="id">Campo ID necessário para apagar um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja apagado com sucesso</response>
    /// <response code="400">Caso ocorra algum erro nas validações para a exclusão</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        _filmeContext.Remove(filme);
        _filmeContext.SaveChanges();

        return NoContent();
    }
}