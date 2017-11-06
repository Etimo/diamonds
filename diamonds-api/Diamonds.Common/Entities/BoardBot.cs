using System;
using Diamonds.Common.Entities;

namespace Diamonds.Common.Entities
{
    public class BoardBot
    {
        public string Name { get; set; }
        public Position Base { get; set; }
        public Position Position { get; set; }

        public int Diamonds { get; set; }
        public DateTime TimeJoined { get; set; }
        public int MillisecondsLeft
        {
            get
            {
                if (TimeJoined == null)
                {
                    return Board.TotalGameTime;
                }
                return GetTimeLeft();
            }
            set{

            }
        }
        public int Score { get; set; }
        public string BotId { get; set; }
        public DateTime NextMoveAvailableAt { get; set; }

        public int GetTimeLeft()
        {
            var endTime = TimeJoined.AddMilliseconds(Board.TotalGameTime);
            var span = (endTime - DateTime.UtcNow);
            return (int)span.TotalMilliseconds;
        }

        public bool CanMove()
        {
            return DateTime.UtcNow >= NextMoveAvailableAt;
        }

        public bool IsGameOver()
        {
            var endTime = TimeJoined.AddMilliseconds(Board.TotalGameTime);
            var isGameOver = (DateTime.UtcNow > endTime);

            return isGameOver;
        }

    }

}
