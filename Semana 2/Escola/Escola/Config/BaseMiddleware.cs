namespace Escola.Config
{
    public class BaseMiddleware
    {
        private readonly RequestDelegate _next;
        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Tudo escrito aqui será executado antes de chamar a controller da api

            await _next(context);
            // Tudo escrito aqui será executado depois de chamar a controller da api
        }
    }
}
