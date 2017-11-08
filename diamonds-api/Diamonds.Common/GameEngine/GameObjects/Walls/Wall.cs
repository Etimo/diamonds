using Diamonds.Common.Entities;

namespace Diamonds.Common.GameEngine.GameObjects.Walls{
    public class Wall : BaseGameObject
    {
        public Wall(Position position){
            this.Position = position;
        }
        public const string NameString = "Wall";
        public override Position Position { get; set; }
        public override bool IsBlocking { get ; set ; } = true;

        public override string Name =>  NameString;

        public override void PerformInteraction(Board board, BoardBot bot)
        {
            //No interaction, just block.
        }
    }

}