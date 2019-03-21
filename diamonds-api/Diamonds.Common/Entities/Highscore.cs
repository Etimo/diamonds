using System;

namespace Diamonds.Common.Entities
{
    public class Highscore
    {
        public string Id { get; set; }
        public string BotName { get; set;}
        public int Score { get; set;}
        // The instant that the bot session on, in UTC.
        public DateTime? SessionFinishedAt { get; set; }

        public Season Season {
            get {
                return Season.All.Find(season => SessionFinishedAt == null || season.EndsAt == null || SessionFinishedAt < season.EndsAt);
            }
        }
    }

}
