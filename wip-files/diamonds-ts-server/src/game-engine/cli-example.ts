import { Board } from "./board";
import { BoardConfig } from "./board-config";
import { DiamondButtonProvider } from "./game-objects/diamond-button/diamond-button-provider";
import { BaseProvider } from "./game-objects/base/base-provider";
import { BotProvider } from "./game-objects/bot/bot-provider";
import { DiamondProvider } from "./game-objects/diamond/diamond-provider";
import { DummyBotProvider } from "./game-objects/dummy-bot/dummy-bot-provider";
import { CustomLogger } from "src/common/logger";

// log.debug("init");

const providers = [
  new DiamondButtonProvider(),
  new BaseProvider(),
  new DiamondProvider(),
  // new TeleportProvider(), // Skip teleports until fully implemented
  new BotProvider(),
  new DummyBotProvider(),
];
const config: BoardConfig = {
  diamondsGenerationRatio: 0.1,
  height: 10,
  width: 10,
  minimumDelayBetweenMoves: 100,
  maxCarryingDiamonds: 5,
};
const board = new Board(config, providers, new CustomLogger());

const bot1 = {
  id: "id1",
};
board.join(bot1);
console.log(board.toString());
