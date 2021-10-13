using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FoodDeliveryTests.Services
{
    public class RestaurantServiceTest
    {
        [Test]
        public void GetRestaurant_Success()
        {
            var temp = new Restaurant() { Latitude = 45.5, Longitude = 50.3 };

            // Arrange
            var readRepositoryStub = new Mock<IReadRestaurantRepository>();
            readRepositoryStub.Setup(x => x.GetRestaurant(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteRestaurantRepository>();
            var service = new RestaurantService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var result = service.GetRestaurant(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetRestaurant_Throws_RestaurantNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadRestaurantRepository>();
            readRepositoryStub.Setup(x => x.GetRestaurant(It.IsAny<Guid>())).Returns((Restaurant)null);
            var writeRepositoryStubStub = new Mock<IWriteRestaurantRepository>();
            var service = new RestaurantService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.GetRestaurant(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Restaurant not found"));
        }

        [Test]
        public void DeleteRestaurant_Success()
        {
            var temp = new Restaurant() { Latitude = 45.5, Longitude = 50.3 };

            // Arrange
            var readRepositoryStub = new Mock<IReadRestaurantRepository>();
            readRepositoryStub.Setup(x => x.GetRestaurant(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteRestaurantRepository>();
            writeRepositoryStubStub.Setup(x => x.DeleteRestaurant(temp));
            var service = new RestaurantService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            service.DeleteRestaurant(Guid.NewGuid());
        }

        [Test]
        public void DeleteRestaurant_Throws_RestaurantNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadRestaurantRepository>();
            readRepositoryStub.Setup(x => x.GetRestaurant(It.IsAny<Guid>())).Returns((Restaurant)null);
            var writeRepositoryStubStub = new Mock<IWriteRestaurantRepository>();
            var service = new RestaurantService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.DeleteRestaurant(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Restaurant not found"));
        }

        [Test]
        public void DeleteRestaurant_Throws_RestaurantIsNotAvailable()
        {
            var temp = new Restaurant() { Latitude = 45.5, Longitude = 50.3, IsAvailableForOrdering= false };

            // Arrange
            var readRepositoryStub = new Mock<IReadRestaurantRepository>();
            readRepositoryStub.Setup(x => x.GetRestaurant(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteRestaurantRepository>();
            writeRepositoryStubStub.Setup(x => x.DeleteRestaurant(temp));
            var service = new RestaurantService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<AppException>(() => service.DeleteRestaurant(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Restaurant cannot be deleted while it is delivering order."));
        }
    }
}
