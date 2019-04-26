using Diamonds.Common.Enums;
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
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Diamonds.Tests.ApiTests
{
    public class BoardsControllerTests
    {
        private readonly ITestOutputHelper output;

        public BoardsControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CanGetBoards()
        {
            // Arrange
            var controller = new BoardsController(new MemoryStorage(), null, null, null);

            // Act
            var response = controller.GetAsync().Result;

            // Assert
            var versionResult = (OkObjectResult)response;
            var boards = (IEnumerable<Board>)versionResult.Value;
            Assert.Equal(1, boards.Count());
            Assert.Equal(3, boards.First().Bots.Count());
            Assert.Equal(8, boards.First().Diamonds.Count());
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
                GameObjects = new List<BaseGameObject> { },
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
                Diamonds = new List<DiamondPosition>
                {
                }
            };
            storage.UpdateBoardAsync(testBoard).Wait();
            var generatorService = new GameObjectGeneratorService();
            Assert.NotEmpty(generatorService.getCurrentObjectGenerators());
            var controller = new BoardsController(storage,
             null,
             new DiamondGeneratorService()
             , generatorService);
             //GameObject related tests here. TODO: Break out into separate test-cases.
            var boardResult = controller.GetBoardAsync("2").Result as OkObjectResult;
            var board = boardResult?.Value as Board;
            Assert.NotEmpty(board.GameObjects);
            Assert.Equal(2,
             board.GameObjects.Where(go =>
                 go.Name.Equals("Teleporter")).Count()
             );
            Assert.Equal(1,
             board.GameObjects.Where(go =>
                 go.Name.Equals("DiamondButton")).Count()
             );
            Assert.False(
             board.GameObjects.Where(go =>
                 go.Name.Equals("DiamondButton")).First().IsBlocking
            );
            List<DiamondPosition> oldDiamonds = new List<DiamondPosition>(board.Diamonds);
             board.GameObjects.Where(go =>
                 go.Name.Equals("DiamondButton")).First().PerformInteraction(board,board.Bots.First(),Direction.North,new DiamondGeneratorService());
            Assert.False(oldDiamonds.SequenceEqual(board.Diamonds));
            Console.WriteLine("Regeneration in the nation!");
        }
    }
}
