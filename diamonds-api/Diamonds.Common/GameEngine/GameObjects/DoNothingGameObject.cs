
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.Common.GameEngine.GameObjects
{
    public class DoNothingGameObject : BaseGameObject
    {
        public override string Name => "Nothing";

        public override Position Position { get => new Position(0,0); set{} }
        public override bool IsBlocking { get => false; set {} }

        public override void PerformInteraction(Board board, BoardBot bot)
        {
        }
    }
}