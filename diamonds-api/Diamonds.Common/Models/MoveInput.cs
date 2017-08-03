using Diamonds.Common.Enums;

namespace Diamonds.Common.Models {
    public class MoveInput {
        public string botToken;
        public Direction direction;

        public bool isValid() {
            return (botToken != null);
        }
    }

}