namespace WebApi.Middlewares
{
    public class TokenFromCookiesMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenFromCookiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Xss-Protection", "1");
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            string? token = context.Request.Cookies["token"];

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await _next.Invoke(context);
        }
    }
}
