using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetlogixOrderApi.DTO;
using NetlogixOrderApi.Models;
using Newtonsoft.Json;
using System.Text;
using NetlogixOrderApi.Validation;

namespace NetlogixOrderApi.Controllers
{
    [ApiKeyAuthAttribute]
    [ApiController]
    [Route("[controller]")]
    public class RequestOrderController : ControllerBase
    {
        IMapper mapper;
        OrderDb db;
        public RequestOrderController(IMapper _mapper, OrderDb _db)
        {
            mapper = _mapper;
            db = _db;
        }

        [HttpPost]
        [Route("/order")]
        public async Task<IResult> PostOrder()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var orderJsonStr = await reader.ReadToEndAsync();

                var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
                if (!jsonValidation.isValid)
                {
                    return Results.BadRequest(jsonValidation.validationErrors);
                }

                var orderDTO = JsonConvert.DeserializeObject<OrderRequestDTO>(orderJsonStr);
                var order = mapper.Map<OrderRequest>(orderDTO);
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return Results.Created($"/order/{order.OrderId}", orderDTO);
            }
        }


        [HttpGet]
        [Route("/order/{id}")]
        public async Task<OrderRequest?> GetOrder(string id)
        {
            var order = await db.Orders
            .Include(x => x.DeliveryAddress)
            .Include(x => x.Items)
            .Include(x => x.PickupAddress)
            .Where(x => x.OrderId == id).FirstOrDefaultAsync();
            return order;
        }

        [HttpGet]
        [Route("/orders")]
        public async Task<List<OrderRequestDTO>> GetOrders()
        {
            var orders = await db.Orders
            .Include(x => x.DeliveryAddress)
            .Include(x => x.Items)
            .Include(x => x.PickupAddress)
            .ToListAsync();
            var ordersDTO = mapper.Map<List<OrderRequestDTO>>(orders);
            return ordersDTO;
        }
    }

}
