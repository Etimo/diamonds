using Diamonds.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diamonds.Common.GameEngine.Move
{
    public interface IMoveService
    {
        Task<MoveResultCode> MoveAsync(string boardId, string botId, Direction direction);
    }
}
