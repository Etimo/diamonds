import axios from "axios";
import { UPDATE_CURRENT_SEASON, UPDATE_ALL_SEASONS } from "./index";

export const updateCurrentSeason = () => {
  const response = axios.get(`api/highscore?season=current`);

  return {
    type: UPDATE_CURRENT_SEASON,
    payload: response
  };
};

export const updateAllSeasons = () => {
  const response = axios.get(`api/highscore?season=all`);

  return {
    type: UPDATE_ALL_SEASONS,
    payload: response
  };
};
