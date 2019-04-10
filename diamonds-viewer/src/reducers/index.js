import { combineReducers } from "redux";
import boardReducer from "./boardReducer";
import botsReducer from "./botsReducer";

export default combineReducers({
  Board: boardReducer,
  Bots: botsReducer
});
