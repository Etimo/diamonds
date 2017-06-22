using Diamonds.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diamonds.Common.GameEngine.Move
{
    public interface IMoveService
    {
        MoveResultCode Move(string boardId, string botId, Direction direction);
    }
}
