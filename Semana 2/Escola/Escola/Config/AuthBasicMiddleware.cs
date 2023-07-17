using Escola.DTO;
using Escola.Services.Interface;

namespace Escola.Config
{
    public class AuthBasicMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAutenticacaoService _autenticacaoService;
        public AuthBasicMiddleware(RequestDelegate next, IAutenticacaoService autenticacao)
        {
            _autenticacaoService = autenticacao;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!ValidateLogin(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }

        private bool ValidateLogin(HttpContext context)
        {
            try
            {
                var header = context.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString();
                var base64 = header.Split(" ")[1];
                var loginSenhaByte = Convert.FromBase64String(base64);
                var loginSenha = System.Text.Encoding.UTF8.GetString(loginSenhaByte).Split(":");

                var loginDTO = new LoginDTO()
                {
                    User = loginSenha[0],
                    Password = loginSenha[1]
                };

                return _autenticacaoService.Autenticar(loginDTO);
            }
            catch
            {
                return false;
            }
        }
    }
}
