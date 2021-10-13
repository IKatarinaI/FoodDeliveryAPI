using AutoMapper;
using FoodDelivery.Controllers;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FoodDeliveryTests.Controllers
{
    public class FoodControllerTest
    {
        [Test]
        public void Get_Returns_ListOfReadFoodObjects()
        {
            // Arrange
            var serviceStub = new Mock<IFoodService>();
            serviceStub.Setup(x => x.GetFoods()).Returns(It.IsAny<IEnumerable<Food>>());
            var mapperStub = new Mock<IMapper>();
            var controller = new FoodController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = controller.GetFoods();
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetById_Returns_ReadFoodObject()
        {
            // Arrange
            var serviceStub = new Mock<IFoodService>();
            serviceStub.Setup(x => x.GetFood(Guid.NewGuid())).Returns(It.IsAny<Food>());
            var mapperStub = new Mock<IMapper>();
            var controller = new FoodController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = controller.GetFood(Guid.NewGuid());
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
