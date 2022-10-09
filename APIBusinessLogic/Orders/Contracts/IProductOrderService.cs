using APIEntities.Enums;
using APIEntities.OrdersEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIBusinessLogic.Orders.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductOrderService
    {
        /// <summary>
        /// This method takes the product collective data and returns top five records 
        /// </summary>
        public IEnumerable<ProductOrderDetails> GetTopFiveRecords(Product_CollectionOfReponses responses);

        /// <summary>
        /// This method takes the product status and configuration settings and returns product collection from API 
        /// </summary>
        public Task <Product_CollectionOfReponses> GetOrdersByStatus(List<Product_Statuses> statuses,string baseUrl, string OrderApi, string apikey);
        /// <summary>
        /// This method takes the product status and configuration settings and returns all the products that having In_Progress status
        /// </summary>
        public Task <IEnumerable<ProductOrderDetails>> GetAllInProgressProducts(List<Product_Statuses> statuses, string baseUrl, string OrderApi, string apikey);
        
    }
}
