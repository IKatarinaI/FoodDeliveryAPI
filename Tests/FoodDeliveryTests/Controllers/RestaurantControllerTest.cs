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
    public class RestaurantControllerTest
    {
        [Test]
        public void Get_Returns_ListOfReadRestaurantObjects()
        {
            // Arrange
            var serviceStub = new Mock<IRestaurantService>();
            serviceStub.Setup(x => x.GetRestaurants()).Returns(It.IsAny<IEnumerable<Restaurant>>());
            var mapperStub = new Mock<IMapper>();
            var controller = new RestaurantController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = controller.GetRestaurants();
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetById_Returns_ReadRestaurantObject()
        {
            // Arrange
            var serviceStub = new Mock<IRestaurantService>();
            serviceStub.Setup(x => x.GetRestaurant(Guid.NewGuid())).Returns(It.IsAny<Restaurant>());
            var mapperStub = new Mock<IMapper>();
            var controller = new RestaurantController(serviceStub.Object, mapperStub.Object);

            // Act
            var result = controller.GetRestaurant(Guid.NewGuid());
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
