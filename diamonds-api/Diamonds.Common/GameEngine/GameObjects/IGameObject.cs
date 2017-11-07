
using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.Common.GameEngine.GameObjects
{
    public interface IGameObject
    {
        string Name { get; }
        Position Position { get; set; }
        void PerformInteraction(Board board, BoardBot bot);
        bool IsBlocking { get; set; }
    }
}