using Microsoft.EntityFrameworkCore;
using NetlogixOrderApi.Models;

public class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options) : base(options) { }

    public DbSet<OrderRequest> Orders { get; set; }
    public DbSet<DeliveryAddress> DeliveryAddress { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<PickupAddress> PickupAddress { get; set; }

}

