import { UPDATE_BOARD } from "../actions";

const defaultState = [];

export default (state = defaultState, action) => {
  switch (action.type) {
    case UPDATE_BOARD:
      const {
        data: { bots }
      } = action.payload;

      return bots;

    default:
      return state;
  }
};
