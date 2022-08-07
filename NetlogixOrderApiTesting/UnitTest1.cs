using NetlogixOrderApi.Controllers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AutoMapper;
using NetlogixOrderApi.AutoMapperProfile;
using NetlogixOrderApi.DTO;
using System.Text.Json;

namespace NetlogixOrderApiTesting
{
    [TestClass]
    public class UnitTest1
    {
        private DbContextOptions<OrderDb> _contextOptions;
        private IMapper _mapper;

        public UnitTest1()
        {
            _contextOptions = new DbContextOptionsBuilder<OrderDb>()
                .UseInMemoryDatabase("items")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new OrderDb(_contextOptions);

            var mappingConfig = new MapperConfiguration(mc =>
               {
                   mc.AddProfile(new OrderProfile());
               });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [TestMethod]
        public async Task TestGetOrders()
        {
            using var context = new OrderDb(_contextOptions);
            var requestOrderController = new RequestOrderController(_mapper, context);
            var res = await requestOrderController.GetOrders();
            Assert.AreEqual(0, res.Count());

        }

        [TestMethod]
        public async Task TestPost()
        {
            using var context = new OrderDb(_contextOptions);
            var requestOrderController = new RequestOrderController(_mapper, context);

            var orderDTO = new OrderRequestDTO()
            {
                OrderId = "CH-1000",
                RequestedPickupTime = "2022/02/03 07:00:00",
                PickupAddress = new PickupAddressDTO()
                {
                    Unit = "14",
                    Street = "Happy Valley Road",
                    Suburb = "Sunshine Place",
                    City = "Springfield",
                    Postcode = "1023"
                },
                DeliveryAddress = new DeliveryAddressDTO()
                {
                    Unit = "66",
                    Street = "Over the hill street",
                    Suburb = "Mountaintop Place",
                    City = "Shelbyville",
                    Postcode = "2013"
                },
                Items = new[]
                {
                    new ItemDTO()
                    {
                        ItemCode = "AMZ-01",
                        Quantity = 20
                    },
                    new ItemDTO()
                    {
                        ItemCode = "XYZ-02",
                        Quantity = 5
                    }

                },
                PickupInstructions = "Ensure driver signs in before heading to the loading bay",
                DeliveryInstructions = "Items are fragile, take extra care when unloading"
            };

            var jsonString = JsonSerializer.Serialize(orderDTO);
            Console.WriteLine(jsonString);
            var res = await requestOrderController.PostOrder(orderDTO);
            // res is not null
            Assert.IsNotNull(res);

        }

        // [TestMethod]
        // public async Task TestPost2()
        // {
        //     using var context = new OrderDb(_contextOptions);
        //     var requestOrderController = new RequestOrderController(_mapper, context);

        //     var req = "{\"OrderId\":\"CH-1000\",\"RequestedPickupTime\":\"2022/02/03 07:00:00\",\"PickupAddress\":{\"Unit\":\"14\",\"Street\":\"Happy Valley Road\",\"Suburb\":\"Sunshine Place\",\"City\":\"Springfield\",\"Postcode\":\"1023\"},\"DeliveryAddress\":{\"Unit\":\"66\",\"Street\":\"Over the hill street\",\"Suburb\":\"Mountaintop Place\",\"City\":\"Shelbyville\",\"Postcode\":\"2013\"},\"Items\":[{\"ItemCode\":\"AMZ-01\",\"Quantity\":20},{\"ItemCode\":\"XYZ-02\",\"Quantity\":5}],\"PickupInstructions\":\"Ensure driver signs in before heading to the loading bay\",\"DeliveryInstructions\":\"Items are fragile, take extra care when unloading\"}";

        //     var res = await requestOrderController.PostOrder(orderDTO);
        //     // res is not null
        //     Assert.IsNotNull(res);
        // }

    }
}