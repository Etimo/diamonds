using System.Collections.Generic;
using Diamonds.Common.Entities;

using Diamonds.Common.Models;
namespace Diamonds.GameEngine.Mutators{
public interface IMutator {
     Position Modify(Bot bot,Board board,Position targetMove);
}
}