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
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;
        private readonly IAlunoService _alunoService;
        private readonly IMemoryCache _memoryCache;

        public BoletimController(IBoletimService boletimService, IAlunoService alunoService, IMemoryCache memoryCache)
        {
            _boletimService = boletimService;
            _alunoService = alunoService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("criarBoletim")]
        public IActionResult Post([FromBody] BoletimDTO boletimDTO)
        {
            if (_alunoService.ObterPorId(boletimDTO.IdAluno) == null)
            {
                throw new NotFoundException("Aluno não cadastrado");
            }

            try
            {
                var boletim = new Boletim(boletimDTO);
                boletim = _boletimService.Criar(boletim);
                return Ok(new BoletimDTO(boletim));
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
        [Route("obterBoletins")]
        public ActionResult<BoletimDTO> Get()
        {
            try
            {
                var boletins = _boletimService.ObterBoletins();
                IEnumerable<BoletimDTO> boletinsDTOs = boletins.Select(x => new BoletimDTO(x));
                return Ok(boletinsDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("obterBoletim/{id}")]
        public IActionResult GetId([FromRoute] int id)
        {
            try
            {
                BoletimDTO boletim;
                if (!_memoryCache.TryGetValue<BoletimDTO>($"boletim:{id}", out boletim))
                {
                    boletim = new BoletimDTO(_boletimService.ObterPorId(id));
                    _memoryCache.Set<BoletimDTO>($"boletim:{id}", boletim, new TimeSpan(0, 0, 20));
                }
                return Ok(boletim);
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
        [Route("obterBoletimAluno/{idAluno}")]
        public IActionResult GetIdAluno([FromRoute] int idAluno)
        {
            try
            {
                var boletins = _boletimService.ObterPorIdAluno(idAluno);
                return Ok(boletins.Select(x => new BoletimDTO(x)));
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
        [Route("atualizarBoletim/{id}")]
        public IActionResult AtualizaMateria([FromBody] BoletimDTO boletimDTO, [FromRoute] int id, int idAluno)
        {
            try
            {
                var boletim = new Boletim(boletimDTO);
                boletim.Id = id;
                boletim.IdAluno = idAluno;
                if (!ModelState.IsValid) return BadRequest("Dados inválidos, favor verificar o formato obrigatório dos dados!");

                boletim = _boletimService.Atualizar(boletim);
                _memoryCache.Remove($"materia:{id}");

                return Ok(new BoletimDTO(boletim));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletarBoletim/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _boletimService.DeletarBoletim(id);
                _memoryCache.Remove($"boletim:{id}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
