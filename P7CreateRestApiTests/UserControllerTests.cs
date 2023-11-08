using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApiTests
{

    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public async Task AddUser_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(loggerMock.Object, mockRepository.Object);


            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            
        }

        [TestMethod]
        public async Task Validate_ReturnsOkOnValidModel()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(loggerMock.Object, mockRepository.Object);


            var userToValidate = new User { /* set properties */ };
            controller.ModelState.Clear(); // Ensuring ModelState is clear for valid model

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            
        }

        [TestMethod]
        public async Task Validate_ReturnsBadRequestOnInvalidModel()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(loggerMock.Object, mockRepository.Object);


            var userToValidate = new User { /* set properties */ };
            controller.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            
        }

       



        [TestMethod]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();

            var controller = new UserController(loggerMock.Object, mockRepository.Object);


            var userIdToDelete = "1";

            // Act
            var result = await controller.DeleteUser(userIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
           
        }

  
    }
}
