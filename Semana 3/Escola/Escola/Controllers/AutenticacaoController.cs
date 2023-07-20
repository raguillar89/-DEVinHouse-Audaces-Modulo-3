using Escola.DTO;
using Escola.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.Controllers
{
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("logar")]
        [AllowAnonymous]
        public IActionResult Logar(LoginDTO loginDTO)
        {
            if (!_autenticacaoService.Autenticar(loginDTO))
                return Unauthorized("Usuario ou Senha inválidos");

            string token = _autenticacaoService.GerarToken(loginDTO);
            return Ok(token);
        }
    }
}
