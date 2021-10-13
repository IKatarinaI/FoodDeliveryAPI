using AutoMapper;
using FoodDelivery.Authorization.Interface;
using FoodDelivery.Controllers;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FoodDeliveryTests.Controllers
{
    public class UserControllerTest
    {
        [Test]
        public void SignUp_Returns_ReadUserObject()
        {
            // Arrange
            var serviceStub = new Mock<IUserService>();
            serviceStub.Setup(x => x.AddUser(It.IsAny<User>()));
            var mapperStub = new Mock<IMapper>();
            var jwtStub = new Mock<IJwtUtils>();
            var controller = new UserController(serviceStub.Object, mapperStub.Object, jwtStub.Object);

            // Act
            var result = controller.SignUp(It.IsAny<CreateUserObject>());
            var okResult = result as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}