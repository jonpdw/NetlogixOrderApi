using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
// using NetlogixOrderApi.AutoMapperProfile;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo { Title = "Netlogix Technical Assessment ", }));
builder.Services.AddSwaggerGen(options =>
{
    // options.CustomSchemaIds(type => type.Name.EndsWith("DTO") ? type.Name.Replace("DTO", string.Empty) : type.Name);

    // options.DocumentFilter<RemoveSchemasFilter>();
    // above was enabled

    // options.CustomSchemaIds(schemaIdStrategy);

});

// private static string schemaIdStrategy(Type currentClass)
// {
//     string returnedValue = currentClass.Name;
//     if (returnedValue.EndsWith("DTO"))
//         returnedValue = returnedValue.Replace("DTO", string.Empty);
//     return returnedValue;
// }
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


// public class RemoveSchemasFilter : IDocumentFilter
// {
//     public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//     {
//         IDictionary<string, OpenApiSchema> allSchemas = swaggerDoc.Components.Schemas;
//         foreach (KeyValuePair<string, OpenApiSchema> _item in allSchemas)
//         {
//             if (!(_item.Key.Contains("DTO")))
//             {
//                 swaggerDoc.Components.Schemas.Remove(_item.Key);
//             }
//         }
//     }
// }