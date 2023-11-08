using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApiTests
{
    [TestClass]
    public class RuleNameControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsCorrectRuleName()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new RuleName { Id = 1, /* other properties */ });

            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult?.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(RuleName));

            var ruleName = okResult.Value as RuleName;
            Assert.AreEqual(1, ruleName.Id);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var ruleNameToCreate = new RuleName { /* set properties */ };

            // Act
            var result = await controller.Post(ruleNameToCreate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));

            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);
            Assert.IsNotNull(createdAtActionResult.RouteValues);
            Assert.AreEqual(ruleNameToCreate.Id, createdAtActionResult.RouteValues["id"]);
            // Add more assertions based on your specific requirements
        }

        [TestMethod]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var existingRuleName = new RuleName { Id = 1, /* other properties */ };
            var ruleNameToUpdate = new RuleName { Id = 1, /* updated properties */ };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<RuleName>()))
                .Callback<RuleName>(updatedRuleName =>
                {
                    // Verify that the UpdateAsync method was called with the correct parameters
                    Assert.AreEqual(ruleNameToUpdate.Id, updatedRuleName.Id);
                    
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, ruleNameToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
           
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var ruleNameIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    // Verify that the DeleteAsync method was called with the correct parameter
                    Assert.AreEqual(ruleNameIdToDelete, id);
                   
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(ruleNameIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            
        }
    }
}

