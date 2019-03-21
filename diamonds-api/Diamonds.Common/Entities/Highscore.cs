using System;

namespace Diamonds.Common.Entities
{
    public class Highscore
    {
        public string Id { get; set; }
        public string BotName { get; set;}
        public int Score { get; set;}
        // The instant that the bot session on, in UTC.
        public DateTime SessionFinishedAt { get; set; }
    }

}
