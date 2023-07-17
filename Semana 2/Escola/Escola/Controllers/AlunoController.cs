using AutoMapper;
using Escola.DTO;
using Escola.Exceptions;
using Escola.Models;
using Escola.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;
        private readonly IMemoryCache _memoryCache;

        public AlunoController(IAlunoService alunoService, IMemoryCache memoryCache)
        {
            _alunoService = alunoService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("criarAluno")]
        public IActionResult Post([FromBody] AlunoDTO alunoDTO)
        {
            try
            {
                var aluno = new Aluno(alunoDTO);
                aluno = _alunoService.Criar(aluno);
                                return Ok(new AlunoDTO(aluno));
            }
            catch (RegistroDuplicadoException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterAlunos")]
        public ActionResult<AlunoDTO> Get()
        {
            try
            {
                var alunos = _alunoService.ObterAlunos();
                IEnumerable<AlunoDTO> alunosDtos = alunos.Select(x => new AlunoDTO(x));
                return Ok(alunosDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterAluno/{id}")]
        public IActionResult GetId([FromRoute] int id)
        {
            try
            {
                AlunoDTO aluno;
                if (!_memoryCache.TryGetValue<AlunoDTO>($"aluno:{id}", out aluno))
                {
                    aluno = new AlunoDTO(_alunoService.ObterPorId(id));
                    _memoryCache.Set<AlunoDTO>($"aluno:{id}", aluno, new TimeSpan(0, 0, 20));
                }
                return Ok(aluno);
            }
            catch (RegistroDuplicadoException ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        [Route("atualizarAluno/{id}")]
        public IActionResult AtualizaAluno([FromBody] AlunoDTO alunoDTO, [FromRoute] int id)
        {
            try
            {
                var aluno = new Aluno(alunoDTO);
                aluno.Id = id;
                if (!ModelState.IsValid) return BadRequest("Dados inválidos, favor verificar o formato obrigatório dos dados!");

                aluno = _alunoService.Atualizar(aluno);
                _memoryCache.Remove($"aluno:{id}");

                return Ok(new AlunoDTO(aluno));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletarAluno/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _alunoService.DeletarAluno(id);
                _memoryCache.Remove($"aluno:{id}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
