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
        const decimal MinRatioOfDiamonds = 0.005m;
        const decimal MaxRatioOfDiamonds = 0.2m;
        static Random _random = new Random();

        public List<Position> GenerateDiamondsIfNeeded(Board board)
        {
            var numberOfBoardCells = board.Height * board.Width;

            var currentRatioOfDiamonds = (decimal) board.Diamonds.Count() / numberOfBoardCells;

            var shouldGenerateDiamonds = currentRatioOfDiamonds < 2;

            if (shouldGenerateDiamonds == false)
            {
                return board.Diamonds;
            }

            var maxNumberOfDiamonds = (int)(MaxRatioOfDiamonds * numberOfBoardCells);

            var diamondPositions = board.Diamonds.ToList();

            while(diamondPositions.Count < maxNumberOfDiamonds)
            {
                var diamondPositionToAdd = GetPositionOfDiamondToAdd(board, diamondPositions);
                diamondPositions.Add(diamondPositionToAdd);
            }

            return diamondPositions;
        }

        private Position GetPositionOfDiamondToAdd(Board board, List<Position> diamondPositions)
        {
            while (true)
            {
                var randomBoardPosition = GetRandomBoardPosition(board.Height, board.Width);
                var canPutDiamondInPosition = IsPositionEmpty(randomBoardPosition, board, diamondPositions);

                if (canPutDiamondInPosition)
                {
                    return randomBoardPosition;
                }
            }
        }

        private bool IsPositionEmpty(Position position, Board board, IList<Position> diamondPositions)
        {
            var positionHasBot = board.Bots.Any(b => b.Position.Equals(position));
            var positionHasBase = board.Bots.Any(b => b.Base.Equals(position));
            var positionHasDiamond = diamondPositions.Contains(position);

            return positionHasBot == false
                && positionHasBase == false
                && positionHasDiamond == false;
        }

        private Position GetRandomBoardPosition(int height, int width)
        {
            var y = _random.Next(0, height);
            var x = _random.Next(0, width);

            return new Position(x, y);
        }

        private bool IsInRange(decimal numberToCheck, decimal rangeMin, decimal rangeMax)
        {
            return rangeMin <= numberToCheck && numberToCheck <= rangeMax;
        }
    }
}
