using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryTests.Services
{
    public class OrderServiceTest
    {
        [Test]
        public void GetOrder_Success()
        {
            var temp = new Order() { OrderedFood = new List<Food>() };

            // Arrange
            var readRepositoryStub = new Mock<IReadOrderRepository>();
            readRepositoryStub.Setup(x => x.GetOrder(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteOrderRepository>();
            var readRestaurantRepositoryStub = new Mock<IReadRestaurantRepository>();
            var writeRestaurantRepositoryStub = new Mock<IWriteRestaurantRepository>();
            var readUserRepositoryStub = new Mock<IReadUserRepository>();

            var service = new OrderService(readRepositoryStub.Object, writeRepositoryStubStub.Object,
                                           readRestaurantRepositoryStub.Object, writeRestaurantRepositoryStub.Object,
                                           readUserRepositoryStub.Object);

            // Act
            var result = service.GetOrder(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetOrder_Throws_OrderNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadOrderRepository>();
            readRepositoryStub.Setup(x => x.GetOrder(It.IsAny<Guid>())).Returns((Order)null);
            var writeRepositoryStubStub = new Mock<IWriteOrderRepository>();
            var readRestaurantRepositoryStub = new Mock<IReadRestaurantRepository>();
            var writeRestaurantRepositoryStub = new Mock<IWriteRestaurantRepository>();
            var readUserRepositoryStub = new Mock<IReadUserRepository>();

            var service = new OrderService(readRepositoryStub.Object, writeRepositoryStubStub.Object,
                                           readRestaurantRepositoryStub.Object, writeRestaurantRepositoryStub.Object,
                                           readUserRepositoryStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.GetOrder(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Order not found"));
        }
    }
}
