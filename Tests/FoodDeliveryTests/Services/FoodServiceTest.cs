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
    public class FoodServiceTest
    {
        [Test]
        public void GetFood_Success()
        {
            var temp = new Food() { Name="Chimichangas" , Price=380 };

            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var result = service.GetFood(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetFood_Throws_FoodNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns((Food)null);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.GetFood(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Food not found"));
        }

        [Test]
        public void DeleteFood_Success()
        {
            var temp = new Food() { Name = "Chimichangas", Price = 380 };

            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            writeRepositoryStubStub.Setup(x => x.DeleteFood(temp));
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            service.DeleteFood(Guid.NewGuid());
        }

        [Test]
        public void DeleteFood_Throws_FoodNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns((Food)null);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.DeleteFood(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Food not found"));
        }

        [Test]
        public void AddFood_Success()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            writeRepositoryStubStub.Setup(x => x.AddFood(It.IsAny<Food>()));
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            service.AddFood(It.IsAny<Food>());
        }

        [Test]
        public void UpdateFood_Success()
        {
            var temp = new Food() { Name = "Chimichangas", Price = 380 };

            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            temp.Rating = 6;
            writeRepositoryStubStub.Setup(x => x.UpdateFood(temp));
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            service.UpdateFood(temp);
        }

        [Test]
        public void UpdateFood_Throws_FoodNotFound()
        {
            var temp = new Food() { Name = "Chimichangas", Price = 380 };

            // Arrange
            var readRepositoryStub = new Mock<IReadFoodRepository>();
            readRepositoryStub.Setup(x => x.GetFood(It.IsAny<Guid>())).Returns((Food)null);
            var writeRepositoryStubStub = new Mock<IWriteFoodRepository>();
            writeRepositoryStubStub.Setup(x => x.UpdateFood(It.IsAny<Food>()));
            var service = new FoodService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.UpdateFood(temp));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Food not found"));
        }
    }
}
