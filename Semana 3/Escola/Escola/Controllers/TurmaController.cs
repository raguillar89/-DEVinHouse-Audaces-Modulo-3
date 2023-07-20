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
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IMemoryCache _memoryCache;

        public TurmaController(ITurmaService turmaService, IMemoryCache memoryCache)
        {
            _turmaService = turmaService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("criarTurma")]
        public IActionResult Post([FromBody] TurmaDTO turmaDTO)
        {
            try
            {
                var turma = new Turma(turmaDTO);
                turma = _turmaService.Criar(turma);
                return Ok(new TurmaDTO(turma));
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
        [Route("obterTurmas")]
        public ActionResult<TurmaDTO> Get()
        {
            try
            {
                var turmas = _turmaService.ObterTurmas();
                IEnumerable<TurmaDTO> turmasDTOs = turmas.Select(x => new TurmaDTO(x));
                return Ok(turmasDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterTurma/{id}")]
        public IActionResult GetId([FromRoute] int id)
        {
            try
            {
                TurmaDTO turma;
                if (!_memoryCache.TryGetValue<TurmaDTO>($"turma:{id}", out turma))
                {
                    turma = new TurmaDTO(_turmaService.ObterPorId(id));
                    _memoryCache.Set<TurmaDTO>($"turma:{id}", turma, new TimeSpan(0, 0, 20));
                }
                return Ok(turma);
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
        [Route("atualizarTurma/{id}")]
        public IActionResult AtualizaAluno([FromBody] TurmaDTO turmaDTO, [FromRoute] int id)
        {
            try
            {
                var turma = new Turma(turmaDTO);
                turma.Id = id;
                if (!ModelState.IsValid) return BadRequest("Dados inválidos, favor verificar o formato obrigatório dos dados!");

                turma = _turmaService.Atualizar(turma);
                _memoryCache.Remove($"turma:{id}");

                return Ok(new TurmaDTO(turma));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletarTurma/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _turmaService.DeletarTurma(id);
                _memoryCache.Remove($"turma:{id}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
