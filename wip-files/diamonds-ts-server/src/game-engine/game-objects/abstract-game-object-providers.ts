import { Board } from "../board";
import { AbstractGameObject } from "./abstract-game-object";
import Bot from "src/common/bot-info";

export abstract class AbstractGameObjectProvider {
  onBoardInitialized(board: Board) {}
  onBotJoined(bot: Bot, board: Board) {}
  onBotFinished(bot: Bot, board: Board) {}

  onGameObjectsRemoved(board: Board, gameObjects: AbstractGameObject[]) {}
  onGameObjectsAdded(board: Board, gameObjects: AbstractGameObject[]) {}
}
