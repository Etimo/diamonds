
using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.Models;
using System.Linq;
namespace Diamonds.GameEngine.GameObjects.Teleporter{
public class TeleporterGenerator  : IGameObjectGenerator<Teleporter>{
    private  const int TeleporterPairs  = 1;
     public List<Teleporter> RegenerateObjects(Board board){

        List<IGameObject> gameObjects = 
         board.GameObjects.Where(gf=>
         { return (gf is Teleporter ? false : true);}).ToList(); //List with no teleporters.
         List<Teleporter> teleporters = new List<Teleporter>();
        for(int i = 0; i< TeleporterPairs;i++){
            Teleporter teleporterOne = new Teleporter();
            Teleporter teleporterTwo = new Teleporter(teleporterOne.LinkedTeleporterString);
            teleporterOne.Position = board.GetRandomEmptyPosition();
            teleporterTwo.Position = board.GetRandomEmptyPosition();
            teleporters.Add(teleporterOne);
            teleporters.Add(teleporterTwo);
        }
            gameObjects.AddRange(teleporters);
            board.GameObjects = gameObjects; //Modifies board,
            return teleporters;
     }
     public List<Position> GetObjectPositions(Board board){
         return board.GameObjects.Where(go => go is Teleporter)
            .Select(go=>go.Position).ToList(); //TODO: Figure out good constant stor in dotnet.
     }
     public List<Teleporter> GetGameObjectList(Board board){
         return board.GameObjects.Where(go => go is Teleporter)
             .Select(go => go as Teleporter).ToList();
     }

}
}