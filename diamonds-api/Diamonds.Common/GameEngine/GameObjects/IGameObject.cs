
using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.GameEngine.GameObjects.Teleporters;
using Diamonds.Common.Models;
namespace Diamonds.Common.GameEngine.GameObjects
{
    [MongoDB.Bson.Serialization.Attributes.BsonDiscriminator(RootClass = true)]
[MongoDB.Bson.Serialization.Attributes.BsonKnownTypes(typeof(Teleporter))]

    public abstract class IGameObject
    {
        public abstract string Name { get; }
        public abstract Position Position { get; set; }
         public abstract void PerformInteraction(Board board, BoardBot bot);
        public abstract bool IsBlocking { get; set; }
    }
}