
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.GameEngine.GameObjects{
public interface IGameObject {
     string Name {get;}
     Position Position{get;set;}
     List<Position> GetObjectPositions(Board board);
     List<T> GetGameObjectList(Board board);
}