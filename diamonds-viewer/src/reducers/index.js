import { combineReducers } from "redux";
import boardReducer from "./boardReducer";
import botsReducer from "./botsReducer";
import highScoreReducer from "./highScoreReducer";

export default combineReducers({
  board: boardReducer,
  bots: botsReducer,
  highScores: highScoreReducer
});
