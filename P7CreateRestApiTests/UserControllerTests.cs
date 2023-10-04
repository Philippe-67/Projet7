using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var controller = new UserController(mockRepository.Object);

            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Validate_ReturnsOkOnValidModel()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockRepository.Object);

            var userToValidate = new User { /* set properties */ };
            controller.ModelState.Clear(); // Ensuring ModelState is clear for valid model

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Validate_ReturnsBadRequestOnInvalidModel()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockRepository.Object);

            var userToValidate = new User { /* set properties */ };
            controller.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task ShowUpdateForm_ReturnsOkWithUser()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockRepository.Object);

            var userId = "1";
            mockRepository.Setup(repo => repo.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId, /* other properties */ });

            // Act
            var result = await controller.ShowUpdateForm(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(User));

            var user = okResult.Value as User;
            Assert.AreEqual(userId, user.Id);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task UpdateUser_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockRepository.Object);

            var userId = "1";
            var userToUpdate = new User { Id = userId, /* updated properties */ };

            // Act
            var result = await controller.UpdateUser(userId, userToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockRepository.Object);

            var userIdToDelete = "1";

            // Act
            var result = await controller.DeleteUser(userIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
            // Add more assertions based on your specific requirements
        }

    //    [TestMethod]
    //    public async Task GetAllUserArticles_ReturnsOkWithListOfUsers()
    //    {
    //        // Arrange
    //        var mockRepository = new Mock<IUserRepository>();
    //        var controller = new UserController(mockRepository.Object);

    //        var users = new List<User> { /* add some users */ };
    //        mockRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

    //        // Act
    //        var result = await controller.GetAllUserArticles();

    //        // Assert
    //        Assert.IsNotNull(result);
    //        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

    //         var okResult = result as OkObjectResult;
    //        Assert.IsNotNull(okResult?.Value);

    //        Assert.IsInstanceOfType(okResult.Value, typeof(List<User>));

    //        var returnedUsers = okResult.Value as List<User>;
    //        CollectionAssert.AreEqual(users, returnedUsers);
    //        // Add more assertions based on your specific requirements
    //    }
    }
}
