using Escola.DTO;
using Escola.Models;
using Escola.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Escola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IConfiguration configuration,IUsuarioService usuarioService) : base(configuration)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<UsuarioGETDTO> Post(UsuarioDTO usuario)
        {
            var usuarioDB = _usuarioService.Criar(new Usuario(usuario));


            return Created(Request.PathBase, new UsuarioGETDTO(usuarioDB));
        }
        [HttpPut("{login}")]
        [Authorize(Roles = "Professor")]
        public ActionResult<UsuarioGETDTO> Put(UsuarioDTO usuario, string login)
        {
            usuario.User = login;
            var usuarioDB = _usuarioService.Atualizar(new Usuario(usuario));


            return Ok(new UsuarioGETDTO(usuarioDB));
        }
        [HttpGet]
        [Authorize(Roles = "Professor,Aluno")]
        public ActionResult<List<UsuarioGETDTO>> Get()
        {
            var usuarios = _usuarioService.ObterUsuarios();


            return Ok(usuarios.Select(x => new UsuarioGETDTO(x)));
        }
        [HttpGet("{login}")]
        [Authorize(Roles = "Professor,Aluno")]
        public ActionResult<List<UsuarioGETDTO>> Get(string login)
        {
            var usuarios = _usuarioService.ObterPorId(login);


            return Ok(new UsuarioGETDTO(usuarios));
        }
        [HttpDelete("{login}")]
        [Authorize(Roles = "Professor")]
        public ActionResult<List<UsuarioGETDTO>> Deletar(string login)
        {
            _usuarioService.DeletarUsuario(login);
            return NoContent();
        }
    }
}
