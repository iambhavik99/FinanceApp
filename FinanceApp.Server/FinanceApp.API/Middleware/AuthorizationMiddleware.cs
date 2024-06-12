using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceApp.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (context.Request.Method == "POST"
                && (context.Request.Path.ToString() == "/api/users/login" || context.Request.Path.ToString() == "/api/users/signup"))
            {
                await next(context);
                return;
            }

            // Check if the user is authenticated
            if (context.User.Identity.IsAuthenticated)
            {
                // Validate the claims (you can add your own custom validation logic here)
                var emailClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var nameClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                var idClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor);

                if (emailClaim != null && nameClaim != null && idClaim != null)
                {
                    // User is authenticated and claims are valid
                    await next(context);
                    return;
                }
            }

            // If authentication fails, return 401 Unauthorized
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("User not authorized.");
            return;
        }
    }
}
