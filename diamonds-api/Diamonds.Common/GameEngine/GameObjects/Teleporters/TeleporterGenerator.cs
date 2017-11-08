
using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using Diamonds.Common.Models;
using System.Linq;
namespace Diamonds.Common.GameEngine.GameObjects.Teleporters{
public class TeleporterGenerator : IGameObjectGenerator {
    private  const int TeleporterPairs  = 1;
     public List<BaseGameObject> RegenerateObjects(Board board){

         List<BaseGameObject> teleporters = new List<BaseGameObject>();
        for(int i = 0; i< TeleporterPairs;i++){
            Teleporter teleporterOne = new Teleporter();
            Teleporter teleporterTwo = new Teleporter(teleporterOne.LinkedTeleporterString);
            teleporterOne.Position = board.GetRandomEmptyPosition();
            teleporterTwo.Position = board.GetRandomEmptyPosition();
            while(teleporterOne.Position.Equals(teleporterTwo.Position)){
                teleporterTwo.Position = board.GetRandomEmptyPosition(); //Could be same slot.
            }
            teleporters.Add(teleporterOne);
            teleporters.Add(teleporterTwo);
        }
            //gameObjects.AddRange(teleporters);
            //board.GameObjects = gameObjects; //Modifies board,
            return teleporters;
     }
     public List<Position> GetObjectPositions(Board board){
         return board.GameObjects.Where(go => go is Teleporter)
            .Select(go=>go.Position).ToList(); //TODO: Figure out good constant stor in dotnet.
     }
     public List<BaseGameObject> GetGameObjectList(Board board){
         return board.GameObjects.Where(go => go is Teleporter)
             .ToList();
     }

}
}