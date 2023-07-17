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
    public class MateriaController : Controller
    {
        private readonly IMateriaService _materiaService;
        private readonly IMemoryCache _memoryCache;

        public MateriaController(IMateriaService materiaService, IMemoryCache memoryCache)
        {
            _materiaService = materiaService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("criarMateria")]
        public IActionResult Post([FromBody] MateriaDTO materiaDTO)
        {
            try
            {
                var materia = new Materia(materiaDTO);
                materia = _materiaService.Criar(materia);
                return Ok(new MateriaDTO(materia));
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
        [Route("obterMaterias")]
        public ActionResult<MateriaDTO> Get()
        {
            try
            {
                var materias = _materiaService.ObterMaterias();
                IEnumerable<MateriaDTO> materiasDTOs = materias.Select(x => new MateriaDTO(x));
                return Ok(materiasDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterMateria/{id}")]
        public IActionResult GetId([FromRoute] int id)
        {
            try
            {
                MateriaDTO materia;
                if (!_memoryCache.TryGetValue<MateriaDTO>($"materia:{id}", out materia))
                {
                    materia = new MateriaDTO(_materiaService.ObterPorId(id));
                    _memoryCache.Set<MateriaDTO>($"materia:{id}", materia, new TimeSpan(0, 0, 20));
                }
                return Ok(materia);
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
        [Route("obterMateria/{nome}")]
        public IActionResult GetNome([FromRoute] string nome)
        {
            try
            {
                MateriaDTO materia;
                if (!_memoryCache.TryGetValue<MateriaDTO>($"materia:{nome}", out materia))
                {
                    materia = new MateriaDTO(_materiaService.ObterPorNome(nome));
                    _memoryCache.Set<MateriaDTO>($"materia:{nome}", materia, new TimeSpan(0, 0, 20));
                }
                return Ok(materia);
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
        [Route("atualizarMateria/{id}")]
        public IActionResult AtualizaMateria([FromBody] MateriaDTO materiaDTO, [FromRoute] int id)
        {
            try
            {
                var materia = new Materia(materiaDTO);
                materia.Id = id;
                if (!ModelState.IsValid) return BadRequest("Dados inválidos, favor verificar o formato obrigatório dos dados!");

                materia = _materiaService.Atualizar(materia);
                _memoryCache.Remove($"materia:{id}");

                return Ok(new MateriaDTO(materia));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletarMateria/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _materiaService.DeletarMateria(id);
                _memoryCache.Remove($"materia:{id}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
