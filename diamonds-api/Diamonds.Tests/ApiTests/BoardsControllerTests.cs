using Diamonds.Common.Entities;
using Diamonds.Common.Storage;
using Diamonds.Rest.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Diamonds.Tests.ApiTests
{

    public  class BoardsControllerTests
    {
        [Fact]
        public void CanGetBoards()
        {
            // Arrange
            var controller = new BoardsController(new MemoryStorage(), null, null);

            // Act
            var response = controller.Get();        

            // Assert
            var versionResult = (OkObjectResult)response;
            var boards = (IEnumerable<Board>) versionResult.Value;

            Assert.Equal(1, boards.Count());
            Assert.Equal(3, boards.First().Bots.Count());
            Assert.Equal(7, boards.First().Diamonds.Count());
        }
    }
}
