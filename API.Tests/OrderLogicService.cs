using APIBusinessLogic.Orders;
using APIBusinessLogic.Orders.Contracts;
using APIEntities.OrdersEntity;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Xunit;

namespace API.Tests
{
    public class OrderLogicService
    {
        private readonly IProductOrderService _orderService;
        public OrderLogicService()
        {
            _orderService = new ProductOrderServices();
        }
        [Fact]
        public void GetTopFiveOrdersRecord_ShouldReturn_CountFive()
        {

            var result = _orderService.GetTopFiveRecords(GetRecords());
            if (result != null)
                Assert.InRange(result.Count(), 1, 5);

        }

        public Product_CollectionOfReponses GetRecords()
        {
            var enviroment = System.Environment.CurrentDirectory;
            var path= Directory.GetParent(enviroment).Parent.Parent.FullName;
            var actualpath=System.IO.Path.Combine(path, @"MockData.json");
            using (StreamReader r = new StreamReader(actualpath))
            {
                string json = r.ReadToEnd();
                var rop = JsonConvert.DeserializeObject<Product_CollectionOfReponses>(json);
                return rop;
            }
        }
    }
}
