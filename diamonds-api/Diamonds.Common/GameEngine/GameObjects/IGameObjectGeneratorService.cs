
using System.Collections.Generic;

namespace Diamonds.Common.GameEngine.GameObjects{
    public interface IGameObjectGeneratorService
    {
        List<IGameObjectGenerator> getCurrentObjectGenerators();
    } 
    }