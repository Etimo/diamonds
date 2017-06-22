using Diamonds.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diamonds.Common.GameEngine.DiamondGenerator
{
    public interface IDiamondGeneratorService
    {
        IEnumerable<Position> GenerateDiamondsIfNeeded(Board board);
    }
}
