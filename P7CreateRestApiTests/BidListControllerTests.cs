using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApiTests
{
    [TestClass]
    public class BidListControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsOkObjectResult()
        {
            // Arrange
            var bidListRepositoryMock = new Mock<IBidListRepository>();
            bidListRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(new BidList { BidListId = 1 });

            var loggerMock = new Mock<ILogger<BidListController>>();

            var controller = new BidListController(loggerMock.Object, bidListRepositoryMock.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var bidListRepositoryMock = new Mock<IBidListRepository>();
            var loggerMock = new Mock<ILogger<BidListController>>();

            var controller = new BidListController(loggerMock.Object, bidListRepositoryMock.Object);
            // Act
            var result = await controller.Post(new BidList());

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task Put_ReturnsNoContentResult()
        {
            // Arrange
            var bidListRepositoryMock = new Mock<IBidListRepository>();
            var loggerMock = new Mock<ILogger<BidListController>>();

            var controller = new BidListController(loggerMock.Object, bidListRepositoryMock.Object);

            // Act
            var result = await controller.Put(1, new BidList { BidListId = 1 });

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var bidListRepositoryMock = new Mock<IBidListRepository>();
            var loggerMock = new Mock<ILogger<BidListController>>();

            var controller = new BidListController(loggerMock.Object, bidListRepositoryMock.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}