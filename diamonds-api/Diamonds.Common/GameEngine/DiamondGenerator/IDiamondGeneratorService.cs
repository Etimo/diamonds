using Diamonds.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diamonds.Common.GameEngine.DiamondGenerator
{
    public interface IDiamondGeneratorService
    {
        List<Position> GenerateDiamondsIfNeeded(Board board);
    }
}
