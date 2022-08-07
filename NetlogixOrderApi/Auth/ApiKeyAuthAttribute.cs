using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var potentialApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!("SecretKey".Equals(potentialApiKey)))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}