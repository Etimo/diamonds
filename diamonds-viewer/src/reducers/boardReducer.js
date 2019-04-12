import _ from "lodash";
import { IS_FETCHING_BOARD, UPDATE_BOARD } from "../actions";

const defaultState = {
  fetching: false,
  width: 0,
  height: 0,
  rows: [[]]
};

const createEmptyBoard = ({ width, height }) => {
  const rows = [];
  for (let y = 0; y < height; y++) {
    rows.push([]);
    for (let x = 0; x < width; x++) {
      rows[y][x] = {};
    }
  }
  return rows;
};

export default (state = defaultState, action) => {
  switch (action.type) {
    case IS_FETCHING_BOARD:
      return {
        ...state,
        fetching: true
      };
    case UPDATE_BOARD:
      const {
        width,
        height,
        bots,
        gameObjects,
        diamonds
      } = action.payload.data;

      const rows = createEmptyBoard({ width, height });

      // Insert bots into board
      bots.forEach(bot => {
        rows[bot.position.y][bot.position.x] = {
          ...rows[bot.position.y][bot.position.x],
          botName: bot.name
        };
        rows[bot.base.y][bot.base.x] = {
          ...rows[bot.base.y][bot.base.x],
          base: bot.name
        };
      });

      // Insert diamonds into board
      diamonds.forEach(diamond => {
        rows[diamond.y][diamond.x] = {
          ...rows[diamond.y][diamond.x],
          diamond: true,
          points: diamond.points
        };
      });

      // Insert gameObjects into board
      gameObjects.forEach(go => {
        rows[go.position.y][go.position.x] = {
          ...rows[go.position.y][go.position.x],
          goName: go.name,
          go: true
        };
      });

      return {
        ...state,
        width,
        height,
        rows,
        fetching: false
      };

    default:
      return state;
  }
};
