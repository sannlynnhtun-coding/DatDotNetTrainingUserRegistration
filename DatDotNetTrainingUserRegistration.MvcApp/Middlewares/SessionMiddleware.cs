using System.Globalization;

namespace DatDotNetTrainingUserRegistration.MvcApp.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var loginData = context.Session.GetString("Login");
            if (loginData == null && context.Request.Path.ToString().ToLower() != "/login")
            {
                context.Response.Redirect("/Login");
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }
}
