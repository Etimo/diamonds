using System;
using System.Collections.Generic;

namespace Diamonds.Common.Entities
{
    public class Season {
        public string Name { get; set; }
        public DateTime? EndsAt { get; set; }

        public static List<Season> All {
            get {
                var seasons = new List<Season>();
                seasons.Add(new Season {
                    Name = "Pre-Season",
                    EndsAt = DateTime.Parse("2018-03-21 15:00:00"),
                });
                seasons.Add(new Season {
                    Name = "Season 1",
                    EndsAt = null,
                });
                return seasons;
            }
        }

        public static Season Current {
            get {
                return All[All.Count - 1];
            }
        }
    }
}
