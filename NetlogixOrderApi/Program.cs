using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
// using NetlogixOrderApi.AutoMapperProfile;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo { Title = "Netlogix Technical Assessment ", }));
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
// });

builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

app.UseDeveloperExceptionPage();


//if (app.Environment.IsDevelopment())
//{
app.UseSwaggerUI();
app.UseSwagger();
// }


app.UseAuthorization();
//app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"));

app.MapControllers();

app.Run();

public class ApiKeyAuthAtribute : Attribute, IAsyncActionFilter
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

