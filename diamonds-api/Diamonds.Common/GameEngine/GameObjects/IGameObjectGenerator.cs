
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.GameEngine.GameObjects{
public interface IGameObjectGenerator<T> {
     List<T> RegenerateObjects(Board board);
     List<Position> GetObjectPositions(Board board);
     List<T> GetGameObjectList(Board board);
}
}