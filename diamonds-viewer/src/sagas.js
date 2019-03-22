import { delay } from 'redux-saga'
import { put, takeEvery, call } from 'redux-saga/effects'
import axios from 'axios'

import * as actionTypes from './constants/actionTypes'

function* pollForUpdates() {
    while(true) {
        //getting bots, diamonds from api
        const boardId = 1;
        const board = yield call(axios.get, `api/boards/${boardId}`);

        const newGameboard = {
          bots: board.data.bots,
          diamonds: board.data.diamonds,
          gameObjects: board.data.gameObjects,
          boardId: board.data.boardId,
          width: board.data.width,
          height: board.data.height
        }

        yield put({type: actionTypes.BOARD_UPDATE_RECEIVED, gameboard: newGameboard})
        yield delay(250);
    }
}

export function* updateSaga() {
    yield takeEvery(actionTypes.REQUEST_BOARD_UPDATE, pollForUpdates);
}

function* pollForHighscores() {
  while(true) {
    const currentSeasonHighscores = (yield call(axios.get, 'api/highscore?season=current')).data;
    const allSeasonsHighscores = (yield call(axios.get, 'api/highscore?season=all')).data;

    yield put({type: actionTypes.HIGHSCORE_UPDATE_RECEIVED, currentSeason: currentSeasonHighscores, allSeasons: allSeasonsHighscores});
    yield delay(5 * 60 * 1000);
  }
}

export function* highscoreSaga() {
    yield takeEvery(actionTypes.REQUEST_HIGHSCORE_UPDATE, pollForHighscores)
}
