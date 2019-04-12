import axios from "axios";
import { IS_FETCHING_BOARD, UPDATE_BOARD } from "./index";

const updateBoard = boardId => {
  const response = axios.get(`/api/boards/${boardId}`);

  return {
    type: UPDATE_BOARD,
    payload: response
  };
};

const isFetchingBoard = () => {
  return { type: IS_FETCHING_BOARD };
};

export default boardId => dispatch => {
  dispatch(isFetchingBoard());
  try {
    dispatch(updateBoard(boardId));
  } catch (err) {
    console.log("err retreiving board update");
  }
};
