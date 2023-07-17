using Escola.Models;
using Microsoft.AspNetCore.Mvc;

namespace Escola.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly List<TokenCliente> _tokensClientes;
        public BaseController(IConfiguration configuration)
        {
            _tokensClientes = configuration.GetSection("tokenCliente").Get<List<TokenCliente>>();
        }
        protected TokenCliente GetCliente()
        {
            var requestToken = Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;
            return _tokensClientes.FirstOrDefault(x => x.Token == requestToken);

        }
    }
}
