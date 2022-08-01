using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();


var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo { Title = "Netlogix Technical Assessment ", }));
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
builder.Services.AddDbContext<OrderDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);
var app = builder.Build();

app.UseDeveloperExceptionPage();

 //if (app.Environment.IsDevelopment())
 //{
     app.UseSwagger(x => x.SerializeAsV2 = true);
     app.UseSwaggerUI();
//}

app.UseAuthorization();
//app.UseSwagger();
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

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderRequest, OrderRequestDTO>().ReverseMap();
        CreateMap<Item, ItemDTO>().ReverseMap();
        CreateMap<DeliveryAddress, DeliveryAddressDTO>().ReverseMap();
        CreateMap<PickupAddress, PickupAddressDTO>().ReverseMap();
    }
}

public class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options) : base(options) { }

    public DbSet<OrderRequest> Orders { get; set; }
    public DbSet<DeliveryAddress> DeliveryAddress { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<PickupAddress> PickupAddress { get; set; }

}
public class OrderRequest
{
    [Key]
    public int OrderId { get; set; }
    public string RequestedPickupTime { get; set; }
    [ForeignKey("PickupAddressId")]
    public PickupAddress PickupAddress { get; set; }
    [ForeignKey("DeliveryAddressId")]
    public DeliveryAddress DeliveryAddress { get; set; }
    public ICollection<Item> Items { get; set; }
    public string PickupInstructions { get; set; }
    public string DeliveryInstructions { get; set; }
}

public class OrderRequestDTO
{
    public int OrderId { get; set; }
    public string RequestedPickupTime { get; set; }
    public PickupAddressDTO PickupAddress { get; set; }
    public DeliveryAddressDTO DeliveryAddress { get; set; }
    public ICollection<ItemDTO> Items { get; set; }
    public string PickupInstructions { get; set; }
    public string DeliveryInstructions { get; set; }
}

public class DeliveryAddress
{
    public int DeliveryAddressId { get; set; }
    public string Unit { get; set; }
    public string Street { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
}

public class DeliveryAddressDTO
{
    public string Unit { get; set; }
    public string Street { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
}

public class Item
{
    public int ItemId { get; set; }
    public string ItemCode { get; set; }
    public int Quantity { get; set; }
}

public class ItemDTO
{
    public string ItemCode { get; set; }
    public int Quantity { get; set; }
}

public class PickupAddress
{
    public int PickupAddressId { get; set; }
    public string Unit { get; set; }
    public string Street { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
}

public class PickupAddressDTO
{
    public string Unit { get; set; }
    public string Street { get; set; }
    public string Suburb { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
}

