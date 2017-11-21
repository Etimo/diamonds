using System.Collections.Generic;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using Diamonds.Common.GameEngine.GameObjects.Walls;

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
