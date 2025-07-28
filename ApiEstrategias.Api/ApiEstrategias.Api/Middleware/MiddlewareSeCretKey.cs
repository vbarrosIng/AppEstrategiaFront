namespace ApiEstrategias.Api.Middleware
{
    public class MiddlewareSeCretKey
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public MiddlewareSeCretKey(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _secretKey = configuration.GetValue<string>("SecretKey");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("No existe el secret");
                return;
            }

            if (!_secretKey.Equals(extractedKey))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Error");
                return;
            }

            await _next(context);
        }


    }
}
