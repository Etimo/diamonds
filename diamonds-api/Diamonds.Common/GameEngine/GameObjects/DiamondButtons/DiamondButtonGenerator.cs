using System.Collections.Generic;
using System;
using Diamonds.Common.Entities;
using System.Linq;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.Common.GameEngine.GameObjects.DiamondButtons {
    public class DiamondButtonGenerator : IGameObjectGenerator
    {
        public DiamondButtonGenerator(){
        }
        public List<BaseGameObject> GetGameObjectList(Board board)
        {
            return board.GameObjects.Where(go => go is DiamondButton).ToList();
        }

        public List<Position> GetObjectPositions(Board board)
        {
            return board.GameObjects.Where(go => go is DiamondButton)
                .Select(go => go.Position).ToList();
        }

        public List<BaseGameObject> RegenerateObjects(Board board)
        {
            
                 var diamondResetButtons= new List<BaseGameObject>()  {
                    new DiamondButton(board.GetRandomEmptyPosition())
                };

            return diamondResetButtons;
        }
    }


}