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
    public class NotasMateriaController : Controller
    {
        private readonly INotasMateriaService _notasMateriaService;
        private readonly IMemoryCache _memoryCache;

        public NotasMateriaController(INotasMateriaService notasMateriaService, IMemoryCache memoryCache)
        {
            _notasMateriaService = notasMateriaService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("criarNotasMateria")]
        public IActionResult Post([FromBody] NotasMateriaDTO notasMateriaDTO)
        {
            try
            {
                var notasMateria = new NotasMateria(notasMateriaDTO);
                notasMateria = _notasMateriaService.Criar(notasMateria);
                return Ok(new NotasMateriaDTO(notasMateria));
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
        [Route("obterNotasMaterias")]
        public ActionResult<MateriaDTO> Get()
        {
            try
            {
                var notasMaterias = _notasMateriaService.ObterNotasMateria();
                IEnumerable<NotasMateriaDTO> notasMateriasDTOs = notasMaterias.Select(x => new NotasMateriaDTO(x));
                return Ok(notasMateriasDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterNotaMateria/{id}")]
        public IActionResult GetId([FromRoute] int id)
        {
            try
            {
                NotasMateriaDTO notasMateria;
                if (!_memoryCache.TryGetValue<NotasMateriaDTO>($"notasMateria:{id}", out notasMateria))
                {
                    notasMateria = new NotasMateriaDTO(_notasMateriaService.ObterPorId(id));
                    _memoryCache.Set<NotasMateriaDTO>($"notasMateria:{id}", notasMateria, new TimeSpan(0, 0, 20));
                }
                return Ok(notasMateria);
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

        [HttpGet]
        [Route("obterBoletim/{idBoletim}")]
        public IActionResult GetBoletim([FromRoute] int idBoletim)
        {
            try
            {
                NotasMateriaDTO notasMateria;
                if (!_memoryCache.TryGetValue<NotasMateriaDTO>($"notasMateria:{idBoletim}", out notasMateria))
                {
                    notasMateria = new NotasMateriaDTO(_notasMateriaService.ObterPorBoletim(idBoletim));
                    _memoryCache.Set<NotasMateriaDTO>($"notasMateria:{idBoletim}", notasMateria, new TimeSpan(0, 0, 20));
                }
                return Ok(notasMateria);
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
        [Route("atualizarNotasMateria/{id}")]
        public IActionResult AtualizaNotasMateria([FromBody] NotasMateriaDTO notasMateriaDTO, [FromRoute] int id)
        {
            try
            {
                var notasMateria = new NotasMateria(notasMateriaDTO);
                notasMateria.Id = id;
                if (!ModelState.IsValid) return BadRequest("Dados inválidos, favor verificar o formato obrigatório dos dados!");

                notasMateria = _notasMateriaService.Atualizar(notasMateria);
                _memoryCache.Remove($"notasMateria:{id}");

                return Ok(new NotasMateriaDTO(notasMateria));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletarNotasMateria/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _notasMateriaService.DeletarNotasMateria(id);
                _memoryCache.Remove($"notasMateria:{id}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
