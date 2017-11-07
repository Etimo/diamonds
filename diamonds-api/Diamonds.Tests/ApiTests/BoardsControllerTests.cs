using Diamonds.Common.Entities;
using Diamonds.Common.Storage;
using Diamonds.Rest.Controllers;
using Diamonds.GameEngine;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace Diamonds.Tests.ApiTests
{

    public class BoardsControllerTests
    {
        [Fact]
        public void CanGetBoards()
        {
            // Arrange
            var controller = new BoardsController(new MemoryStorage(), null, null, null);

            // Act
            var response = controller.Get();

            // Assert
            var versionResult = (OkObjectResult)response;
            var boards = (IEnumerable<Board>)versionResult.Value;
            Assert.Equal(1, boards.Count());
            Assert.Equal(3, boards.First().Bots.Count());
            Assert.Equal(7, boards.First().Diamonds.Count());
            /**
            * Induce game object generation
             */
        }
        [Fact]
        public void CanGenerateObjects()
        {

            var testGenerators = new List<IGameObjectGenerator>(){
                new TeleporterGenerator() as IGameObjectGenerator
            };
            var storage = new MemoryStorage();
            var testBoard = new Board
            {
                Id = "2",
                Height = 10,
                Width = 10,
                GameObjects = new List<IGameObject> { },
                Bots = new List<BoardBot>{
                    new BoardBot {
                        Name = "Jane Jet",
                        Base = new Position(6, 2),
                        Position = new Position(8, 0),
                        Score = 5,
                        Diamonds = 0,
                    },
                    new BoardBot {
                        Name = "indrif",
                        Base = new Position(5, 8),
                        Position = new Position(0, 0),
                        Score = 2,
                        Diamonds = 2,
                    },
                },
                Diamonds = new List<Position>
                {
                }
            };
            storage.UpdateBoard(testBoard);
            var generatorService = new GameObjectGeneratorService();
            Assert.NotEmpty(generatorService.getCurrentObjectGenerators());
            var controller = new BoardsController(storage,
             null,
             new DiamondGeneratorService()
             , generatorService);
            var boardResult = controller.GetBoard("2") as OkObjectResult;
            var board = boardResult.Value as Board;
            Assert.NotEmpty(board.GameObjects);
            Assert.Equal(2,
             board.GameObjects.Where(go =>
                 go.Name.Equals("Teleporter")).Count()
             );
        }
    }
}
