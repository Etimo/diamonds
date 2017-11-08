using System.Collections.Generic;
using System;
using Diamonds.Common.Entities;
using System.Linq;

namespace Diamonds.Common.GameEngine.GameObjects.Walls {
    public class WallGenerator : IGameObjectGenerator
    {
    private int _maxWalls = 4;
    private int _minWalls = 2; 
    static Random _random = new Random();
        public List<BaseGameObject> GetGameObjectList(Board board)
        {
            return board.GameObjects.Where(go => go is Wall).ToList();
        }

        public List<Position> GetObjectPositions(Board board)
        {
            return board.GameObjects.Where(go => go is Wall)
                .Select(go => go.Position).ToList();
        }

        public List<BaseGameObject> RegenerateObjects(Board board)
        {
            var numWalls = _random.Next(_minWalls,_maxWalls);
            List<BaseGameObject> walls = new List<BaseGameObject>(numWalls);
            //List<Position> posAccumulator = new List<Position>(numWalls);
            while(walls.Count<numWalls){
                var pos = board.GetRandomEmptyPosition();
                if(!walls.Any(wall => wall.Position.Equals(pos))){
                    walls.Add(new Wall(pos));
                }
            }
            return walls;
        }
    }


}