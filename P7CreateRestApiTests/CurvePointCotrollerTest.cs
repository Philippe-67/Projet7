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
    public class CurvePointControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsCorrectCurvePoint()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CurvePoint { Id = 1, /* other properties */ });

            var controller = new CurvePointController(mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(CurvePoint));

            var curvePoint = okResult.Value as CurvePoint;
            Assert.AreEqual(1, curvePoint.Id);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointController(mockRepository.Object);

            var curvePointToCreate = new CurvePoint { /* set properties */ };

            // Act
            var result = await controller.Post(curvePointToCreate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);
            Assert.IsNotNull(createdAtActionResult.RouteValues);
            Assert.AreEqual(curvePointToCreate.Id, createdAtActionResult.RouteValues["id"]);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointController(mockRepository.Object);

            var existingCurvePoint = new CurvePoint { Id = 1, /* other properties */ };
            var curvePointToUpdate = new CurvePoint { Id = 1, /* updated properties */ };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CurvePoint>()))
                .Callback<CurvePoint>(updatedCurvePoint =>
                {
                    // Verify that the UpdateAsync method was called with the correct parameters
                    Assert.AreEqual(curvePointToUpdate.Id, updatedCurvePoint.Id);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, curvePointToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointController(mockRepository.Object);

            var curvePointIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    // Verify that the DeleteAsync method was called with the correct parameter
                    Assert.AreEqual(curvePointIdToDelete, id);
                    // Add more assertions based on your specific requirements
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(curvePointIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            // Add more assertions based on your specific requirements
        }
    }
}
