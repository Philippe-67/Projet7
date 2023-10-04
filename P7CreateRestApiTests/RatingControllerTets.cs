using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace P7CreateRestApiTests
{

    [TestClass]
    public class RatingControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsCorrectRating()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Rating { Id = 1, /* other properties */ });

            var controller = new RatingController(mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Rating));

            var rating = okResult.Value as Rating;
            Assert.AreEqual(1, rating.Id);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockRepository.Object);

            var ratingToCreate = new Rating { /* set properties */ };

            // Act
            var result = await controller.Post(ratingToCreate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);
            Assert.IsNotNull(createdAtActionResult.RouteValues);
            Assert.AreEqual(ratingToCreate.Id, createdAtActionResult.RouteValues["id"]);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockRepository.Object);

            var existingRating = new Rating { Id = 1, /* other properties */ };
            var ratingToUpdate = new Rating { Id = 1, /* updated properties */ };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Rating>()))
                .Callback<Rating>(updatedRating =>
                {
                    // Verify that the UpdateAsync method was called with the correct parameters
                    Assert.AreEqual(ratingToUpdate.Id, updatedRating.Id);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, ratingToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockRepository.Object);

            var ratingIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    // Verify that the DeleteAsync method was called with the correct parameter
                    Assert.AreEqual(ratingIdToDelete, id);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(ratingIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }
    }
}
