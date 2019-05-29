import { AbstractGameObject } from "../abstract-game-object";
import { Board } from "../../board";
import { BotGameObject } from "../bot/bot";
import { DummyBotGameObject } from "../dummy-bot/dummy-bot";
import IPosition from "src/common/position";

export class DiamondGameObject extends AbstractGameObject {
  public readonly type: string = "diamond";

  toChar() {
    return this.points === 1 ? "🔹" : "🔶";
  }

  constructor(position: IPosition, private readonly points) {
    super(position);
  }

  /**
   * Remove the diamond when a bot enters and put it in the bot's inventory.
   */
  onGameObjectEntered(gameObject: AbstractGameObject, board: Board) {
    if (gameObject instanceof BotGameObject) {
      const bot = gameObject as BotGameObject;
      if (bot.diamonds + this.points <= board.getConfig().maxCarryingDiamonds) {
        bot.diamonds += this.points;
        board.removeGameObject(this);
      }
    }
  }
}
