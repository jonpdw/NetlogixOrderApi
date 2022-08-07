using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseAuthorization();

app.MapControllers();

app.Run();