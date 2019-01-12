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
            var mockBookDao = new Mock<IBookDao>();

            mockOrderService.Protected().Setup<IBookDao>("GetBookDao").Returns(mockBookDao.Object);

            mockOrderService.Object.SyncBookOrders();

            mockBookDao.Verify(x => x.Insert(It.Is<Order>(order => order.Type == "Book")), Times.Exactly(2));
            mockBookDao.Verify(x => x.Insert(It.Is<Order>(order => order.Type == "no")), Times.Never);

            // hard to isolate dependency to unit test

            //var target = new OrderService();
            //target.SyncBookOrders();
        }
    }
}