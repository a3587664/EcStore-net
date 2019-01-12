using System.Collections.Generic;
using Moq;
using Moq.Protected;
using NSubstitute;
using NUnit.Framework;

namespace IsolatedByInheritanceAndOverride.Test
{
    /// <summary>
    /// OrderServiceTest 的摘要描述
    /// </summary>
    [TestFixture]
    public class OrderServiceTest
    {
        [Test]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            var mockOrderService = new Mock<OrderService>();
            mockOrderService.Protected().Setup<List<Order>>("GetOrders").Returns(new List<Order>
            {
                new Order
                {
                    Type = "Book"
                },
                new Order
                {
                    Type = "Book"
                },
                new Order
                {
                    Type = "no"
                }
            });
            
            mockOrderService.Protected().Setup<IBookDao>("GetBookDao").Returns(new FakeBookDao());

            mockOrderService.Object.SyncBookOrders();

            // hard to isolate dependency to unit test

            //var target = new OrderService();
            //target.SyncBookOrders();
        }
    }

    public class FakeBookDao : IBookDao
    {
        public void Insert(Order order)
        {
            // directly depend on some web service
            //var client = new HttpClient();
            //var response = client.PostAsync<Order>("http://api.joey.io/Order", order, new JsonMediaTypeFormatter()).Result;

            //response.EnsureSuccessStatusCode();
            //throw new NotImplementedException();
        }
    }
}