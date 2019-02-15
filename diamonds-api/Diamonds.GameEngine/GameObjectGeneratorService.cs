using System.Collections.Generic;
using Diamonds.Common.GameEngine.GameObjects;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using Diamonds.Common.GameEngine.GameObjects.Walls;
using Diamonds.Common.GameEngine.GameObjects.DiamondButtons;
using Diamonds.Common.GameEngine.DiamondGenerator;

namespace Diamonds.GameEngine
{

    public class GameObjectGeneratorService : IGameObjectGeneratorService
    {
        public GameObjectGeneratorService(
           IDiamondGeneratorService boardDiamondManager
        )
        {
            _generators = new List<IGameObjectGenerator>(){
            new TeleporterGenerator(),
            new DiamondButtonGenerator()
        };


        }
        readonly List<IGameObjectGenerator> _generators;
        public List<IGameObjectGenerator> getCurrentObjectGenerators()
        {
            return new List<IGameObjectGenerator>(_generators); //Prevent modification. Unsure about dotnet immutable collections.
        }
    }
}
