using Diamonds.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diamonds.Common.GameEngine.DiamondGenerator
{
    public interface IDiamondGeneratorService
    {
        List<DiamondPosition> GenerateDiamondsIfNeeded(Board board);
        bool NeedToGenerateDiamonds(Board board);

    }
}
