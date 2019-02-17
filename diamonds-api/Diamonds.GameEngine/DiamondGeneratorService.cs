using Diamonds.Common.GameEngine.DiamondGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using Diamonds.Common.Entities;
using System.Linq;

namespace Diamonds.GameEngine
{
    public class DiamondGeneratorService : IDiamondGeneratorService
    {
        const decimal MinRatioOfDiamonds = 0.01m;
        const decimal MaxRatioOfDiamonds = 0.2m;
        const decimal MaxRatioRedDiamonds = 0.5m; // 50% of diamonds generated
        static Random _random = new Random();
        public bool NeedToGenerateDiamonds(Board board)
        {
            var numberOfBoardCells = board.Height * board.Width;
            var currentRatioOfDiamonds = (decimal)board.Diamonds.Count() / numberOfBoardCells;
            return currentRatioOfDiamonds < MinRatioOfDiamonds;
        }

        public List<DiamondPosition> GenerateDiamondsIfNeeded(Board board)
        {
            var numberOfBoardCells = board.Height * board.Width;
            if (NeedToGenerateDiamonds(board) == false)
            {
                return board.Diamonds;
            }

            var maxNumberOfDiamonds = (int)(MaxRatioOfDiamonds * numberOfBoardCells);
            var diamondPositions = board.Diamonds.ToList();
            while (diamondPositions.Count < maxNumberOfDiamonds)
            {
                var diamondPositionToAdd = board.GetRandomEmptyPosition();
                diamondPositions.Add(new DiamondPosition(diamondPositionToAdd));
            }

            // Replace some diamonds with red ones
            var maxRedDiamonds = _random.Next((int)(maxNumberOfDiamonds * MaxRatioRedDiamonds));
            for (var i = 0; i < maxRedDiamonds; i++)
            {
                diamondPositions[i].Points = 2;
            }

            return diamondPositions;
        }

        private bool IsInRange(decimal numberToCheck, decimal rangeMin, decimal rangeMax)
        {
            return rangeMin <= numberToCheck && numberToCheck <= rangeMax;
        }
    }
}
