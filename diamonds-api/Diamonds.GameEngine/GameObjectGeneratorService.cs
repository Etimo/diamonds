using System.Collections.Generic;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;

namespace Diamonds.GameEngine {

    public class GameObjectGeneratorService : IGameObjectGeneratorService
    {
        List<IGameObjectGenerator> _generators = new List<IGameObjectGenerator>(){
            new TeleporterGenerator() 
        };
        public List<IGameObjectGenerator> getCurrentObjectGenerators()
        {
            return new List<IGameObjectGenerator>(_generators); //Prevent modification. Unsure about dotnet immutable collections.
        }
    }
}