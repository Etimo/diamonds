import { AbstractGameObjectProvider } from "../abstract-game-object-providers";
import { Board } from "src/game-engine/board";
import { BotGameObject } from "./bot";
import IPosition from "src/common/position";

export class BotProvider extends AbstractGameObjectProvider {
  onBotJoined(bot: object, board: Board) {
    // Add game object to board
    const base = board.getEmptyPosition();
    const botGameObject = this.getInitializedBot(bot, base);
    board.addGameObjects([botGameObject]);
  }

  private getInitializedBot(data: object, base: IPosition) {
    const botGameObject = new BotGameObject(base);
    botGameObject.base = base;
    botGameObject.timeJoined = new Date();
    botGameObject.diamonds = 0;
    botGameObject.score = 0;
    return botGameObject;
  }
}
