
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
using Diamonds.Common.GameEngine.GameObjects;
namespace Diamonds.Common.GameEngine.GameObjects{
public interface IGameObjectGenerator {//<T> where T : IGameObject {
     List<IGameObject> RegenerateObjects(Board board);
     List<Position> GetObjectPositions(Board board);
     List<IGameObject> GetGameObjectList(Board board);
     /**
     *This would allow a call to for example a machine gun each round, 
     *where it would shoot one bullet per 10th round, or move the bullets in a direction. 
     *Will wait to implement active objects (i.e. machinge guns, moving bullets etc.)
      */
     //void PerformTurnBasedAction(Board board);
    }
}