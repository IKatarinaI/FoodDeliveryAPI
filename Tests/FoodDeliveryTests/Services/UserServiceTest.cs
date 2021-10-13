using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FoodDeliveryTests.Services
{
    public class UserServiceTest
    {
        [Test]
        public void Authenticate_Success()
        {
            var temp = new User() { Username = "username", Password = "password" };
            var user = new User() { Username = "username", Password = BCryptNet.HashPassword("password") };
            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var result = service.Authenticate(temp);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Authenticate_Throws_UserNotFound()
        {
            var temp = new User() { Username = "username", Password = "password" };
            var user = new User() { Username = "username", Password = "username" };

            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<AppException>(() => service.Authenticate(user));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void GetUser_Success()
        {
            var temp = new User() { Username = "username", Password = "password" };

            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.GetUser(It.IsAny<Guid>())).Returns(temp);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var result = service.GetUser(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetUser_Throws_UserNotFound()
        {
            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.GetUser(It.IsAny<Guid>())).Returns((User)null);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<KeyNotFoundException>(() => service.GetUser(It.IsAny<Guid>()));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void Authenticate_Throws_PasswordNotCorrect()
        {
            var user = new User() { Username = "username", Password = "password" };
            var temp = new User() { Username = "username", Password = BCryptNet.HashPassword("username") };

            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)temp);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<AppException>(() => service.Authenticate(user));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Password is not correct"));
        }

        [Test]
        public void AddUser_Success()
        {
            var temp = new User() { Username = "username", Password = "password" };

            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.IsUsernameUnique(It.IsAny<string>())).Returns(false);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            writeRepositoryStubStub.Setup(x => x.AddUser(It.IsAny<User>()));
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            service.AddUser(temp);
        }

        [Test]
        public void AddUser_Throws_UsernameIsAleadyTaken()
        {
            var temp = new User() { Username = "username", Password = "password" };

            // Arrange
            var readRepositoryStub = new Mock<IReadUserRepository>();
            readRepositoryStub.Setup(x => x.IsUsernameUnique(It.IsAny<string>())).Returns(true);
            var writeRepositoryStubStub = new Mock<IWriteUserRepository>();
            writeRepositoryStubStub.Setup(x => x.AddUser(It.IsAny<User>()));
            var service = new UserService(readRepositoryStub.Object, writeRepositoryStubStub.Object);

            // Act
            var ex = Assert.Throws<AppException>(() => service.AddUser(temp));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Username is already taken"));
        }
    }
}
