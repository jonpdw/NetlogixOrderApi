
using NetlogixOrderApi.Validation;

namespace NetlogixOrderApiTesting
{
    [TestClass]
    public class UnitTestPostOrder
    {


        [TestMethod]
        public void EmtptyItems()
        {
            var orderJsonStr = @"
            {
                'OrderId': 'CH-1007',
                'RequestedPickupTime': '2022/02/03 07:00:00',
                'PickupAddress': { 'Unit': '14', 'Street': 'Happy Valley Road', 'Suburb': 'Sunshine Place', 'City': 'Springfield', 'Postcode': '1023' },
                'DeliveryAddress': { 'Unit': '66', 'Street': 'Over the hill street', 'Suburb': 'Mountaintop Place', 'City': 'Shelbyville', 'Postcode': '2013' },
                'Items': [],
                'PickupInstructions': 'Ensure driver signs in before heading to the loading bay',
                'DeliveryInstructions': 'Items are fragile, take extra care when unloading'
            }";
            var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
            Assert.IsTrue(jsonValidation.isValid);
        }

        [TestMethod]
        public void MissingItemQuantity()
        {
            var orderJsonStr = @"
            {
                'OrderId': 'CH-1007',
                'RequestedPickupTime': '2022/02/03 07:00:00',
                'PickupAddress': { 'Unit': '14', 'Street': 'Happy Valley Road', 'Suburb': 'Sunshine Place', 'City': 'Springfield', 'Postcode': '1023' },
                'DeliveryAddress': { 'Unit': '66', 'Street': 'Over the hill street', 'Suburb': 'Mountaintop Place', 'City': 'Shelbyville', 'Postcode': '2013' },
                'Items': [ { 'ItemCode': 'XYZ-02' } ],
                'PickupInstructions': 'Ensure driver signs in before heading to the loading bay',
                'DeliveryInstructions': 'Items are fragile, take extra care when unloading'
            }";
            var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
            Assert.IsFalse(jsonValidation.isValid);
            Assert.AreEqual(jsonValidation.validationErrors.First(), "Required properties are missing from object: Quantity.");
        }

        [TestMethod]
        public void MissingDeliveryAddress()
        {
            var orderJsonStr = @"
            {
                'OrderId': 'CH-1007',
                'RequestedPickupTime': '2022/02/03 07:00:00',
                'PickupAddress': { 'Unit': '14', 'Street': 'Happy Valley Road', 'Suburb': 'Sunshine Place', 'City': 'Springfield', 'Postcode': '1023' },
                'Items': [ { 'ItemCode': 'XYZ-02', Quantity: 5 } ],
                'PickupInstructions': 'Ensure driver signs in before heading to the loading bay',
                'DeliveryInstructions': 'Items are fragile, take extra care when unloading'
            }";
            var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
            Assert.IsFalse(jsonValidation.isValid);
            Assert.AreEqual(jsonValidation.validationErrors.First(), "Required properties are missing from object: DeliveryAddress.");
        }

        [TestMethod]
        public void MissingOrderId()
        {
            var orderJsonStr = @"
            {
                'RequestedPickupTime': '2022/02/03 07:00:00',
                'DeliveryAddress': { 'Unit': '66', 'Street': 'Over the hill street', 'Suburb': 'Mountaintop Place', 'City': 'Shelbyville', 'Postcode': '2013' },
                'PickupAddress': { 'Unit': '14', 'Street': 'Happy Valley Road', 'Suburb': 'Sunshine Place', 'City': 'Springfield', 'Postcode': '1023' },
                'Items': [ { 'ItemCode': 'XYZ-02', Quantity: 5 } ],
                'PickupInstructions': 'Ensure driver signs in before heading to the loading bay',
                'DeliveryInstructions': 'Items are fragile, take extra care when unloading'
            }";
            var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
            Assert.IsFalse(jsonValidation.isValid);
            Assert.AreEqual(jsonValidation.validationErrors.First(), "Required properties are missing from object: OrderId.");
        }

        [TestMethod]
        public void MissingPickupAddressPostcode()
        {
            var orderJsonStr = @"
            {
                'OrderId': 'CH-1007',
                'RequestedPickupTime': '2022/02/03 07:00:00',
                'DeliveryAddress': { 'Unit': '66', 'Street': 'Over the hill street', 'Suburb': 'Mountaintop Place', 'City': 'Shelbyville', 'Postcode': '2013' },
                'PickupAddress': { 'Unit': '14', 'Street': 'Happy Valley Road', 'Suburb': 'Sunshine Place', 'City': 'Springfield' },
                'Items': [ { 'ItemCode': 'XYZ-02', Quantity: 5 } ],
                'PickupInstructions': 'Ensure driver signs in before heading to the loading bay',
                'DeliveryInstructions': 'Items are fragile, take extra care when unloading'
            }";
            var jsonValidation = ControllerValidation.ValidateOrderRequestDTO(orderJsonStr);
            Assert.IsFalse(jsonValidation.isValid);
            Assert.AreEqual(jsonValidation.validationErrors.First(), "Required properties are missing from object: Postcode.");
        }
    }
}