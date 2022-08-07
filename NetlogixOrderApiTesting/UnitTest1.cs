using NetlogixOrderApi.Controllers;

namespace NetlogixOrderApiTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRequestController()
        {
            // inMemoryDatabase implementing OrderDb
            var requestOrderController = new RequestOrderController(null, null);
        }
    }
}