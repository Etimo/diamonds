
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.Common.GameEngine.GameObjects
{
    public class DoNothingGameObject : IGameObject
    {
        public string Name => "Nothing";

        public Position Position { get => new Position(0,0); set{} }
        public bool IsBlocking { get => false; set {} }

        public void PerformInteraction(Board board, BoardBot bot)
        {
        }
    }
}