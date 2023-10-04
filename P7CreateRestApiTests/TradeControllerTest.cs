using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace P7CreateRestApiTests
{

    [TestClass]
    public class TradeControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsCorrectTrade()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Trade { TradeId = 1, /* other properties */ });

            var controller = new TradeController(mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Trade));

            var trade = okResult.Value as Trade;
            Assert.AreEqual(1, trade.TradeId);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradeController(mockRepository.Object);

            var tradeToCreate = new Trade { /* set properties */ };

            // Act
            var result = await controller.Post(tradeToCreate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);
            Assert.IsNotNull(createdAtActionResult.RouteValues);
            Assert.AreEqual(tradeToCreate.TradeId, createdAtActionResult.RouteValues["id"]);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradeController(mockRepository.Object);

            var existingTrade = new Trade { TradeId = 1, /* other properties */ };
            var tradeToUpdate = new Trade { TradeId = 1, /* updated properties */ };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trade>()))
                .Callback<Trade>(updatedTrade =>
                {
                    // Verify that the UpdateAsync method was called with the correct parameters
                    Assert.AreEqual(tradeToUpdate.TradeId, updatedTrade.TradeId);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, tradeToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            var controller = new TradeController(mockRepository.Object);

            var tradeIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    // Verify that the DeleteAsync method was called with the correct parameter
                    Assert.AreEqual(tradeIdToDelete, id);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(tradeIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }
    }
}
