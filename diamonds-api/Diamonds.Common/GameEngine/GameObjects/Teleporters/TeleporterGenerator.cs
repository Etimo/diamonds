using System.Collections.Generic;
using Diamonds.Common.Entities;
using System.Linq;
using System;

namespace Diamonds.Common.GameEngine.GameObjects.Teleporters
{
    public class TeleporterGenerator : IGameObjectGenerator
    {
        private const int TeleporterPairs = 1;
        public List<BaseGameObject> RegenerateObjects(Board board)
        {
            List<BaseGameObject> teleporters = new List<BaseGameObject>();
            for (int i = 0; i < TeleporterPairs; i++)
            {
                Teleporter teleporterOne = new Teleporter();
                Teleporter teleporterTwo = new Teleporter(teleporterOne.LinkedTeleporterString);
                var (teleporterOnePosition, teleporterTwoPosition) = GetRandomTeleporterPositions(board);
                teleporterOne.Position = teleporterOnePosition;
                teleporterTwo.Position = teleporterTwoPosition;
                while (teleporterOne.Position.Equals(teleporterTwo.Position))
                {
                    teleporterTwo.Position = board.GetRandomEmptyPosition(); //Could be same slot.
                }
                teleporters.Add(teleporterOne);
                teleporters.Add(teleporterTwo);
            }
            //gameObjects.AddRange(teleporters);
            //board.GameObjects = gameObjects; //Modifies board,
            return teleporters;
        }
        public List<Position> GetObjectPositions(Board board)
        {
            return board.GameObjects.Where(go => go is Teleporter)
               .Select(go => go.Position).ToList(); //TODO: Figure out good constant stor in dotnet.
        }
        public List<BaseGameObject> GetGameObjectList(Board board)
        {
            return board.GameObjects.Where(go => go is Teleporter)
                .ToList();
        }

        public (Position A, Position B) GetRandomTeleporterPositions(Board board)
        {
            var positionToReturn = Enumerable.Range(0, 3)
            .Select(counter => new
            {
                A = board.GetRandomEmptyPosition(),
                B = board.GetRandomEmptyPosition()
            }
            )
            .OrderByDescending(positions => Math.Abs(positions.A.X - positions.B.X) + Math.Abs(positions.A.Y - positions.B.Y))
            .First();

            return (positionToReturn.A, positionToReturn.B);
        }
    }
}