using Diamonds.Common.Entities;
using System.Collections.Generic;

using Diamonds.Common.Enums;
using Diamonds.Common.GameEngine.DiamondGenerator;
using System.Linq;
using System;

namespace Diamonds.Common.GameEngine.GameObjects.DiamondButtons{
    public class DiamondButton : BaseGameObject
    {
        public DiamondButton(Position position){
            this.Position = position;
        }
        public const string NameString = "DiamondButton";
        public override Position Position { get; set; }
        public override bool IsBlocking { get => false ; set {} }
        public override string Name =>  NameString;

        public override void PerformInteraction(Board board,
         BoardBot bot,
         Direction direction,
        IDiamondGeneratorService generator)
        {
            //reset diamonds here...
            board.Diamonds = new List<DiamondPosition>(); //Trigger board rebuild
            this.Position = board.GetRandomEmptyPosition();
            generator.GenerateDiamondsIfNeeded(board);
        }
        public override void PerformInteraction(Board board,
         BoardBot bot,
         Direction direction
       )
        {
            //Do nothing here. It's alright.
        }
    }

}
