import { UPDATE_CURRENT_SEASON, UPDATE_ALL_SEASONS } from "../actions";

const defaultState = {
  currentSeason: [],
  allSeasons: []
};

export default (state = defaultState, action) => {
  switch (action.type) {
    case UPDATE_CURRENT_SEASON:
      return {
        ...state,
        currentSeason: action.payload.data
      };
    case UPDATE_ALL_SEASONS:
      return {
        ...state,
        allSeasons: action.payload.data
      };
    default:
      return state;
  }
};
