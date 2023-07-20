using Escola.Models;

namespace Escola.Config
{
    public class AuthTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<TokenCliente> _tokens;

        public AuthTokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _tokens = configuration.GetSection("tokenCliente").Get<List<TokenCliente>>();
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
            var requestToken = context.Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;

            return _tokens.Find(x => x.Token == requestToken) != null;
        }
    }
}
