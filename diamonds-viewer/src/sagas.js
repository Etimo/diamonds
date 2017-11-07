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
          gameObjects: board.data.gameObjects,
          diamonds: board.data.diamonds,
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
    const player = yield call(axios.get, 'api/highscore');
    const newHighscores = player.data;

    yield put({type: actionTypes.HIGHSCORE_UPDATE_RECEIVED, highscores: newHighscores});
    yield delay(5000);
  }
}

export function* highscoreSaga() {
    yield takeEvery(actionTypes.REQUEST_HIGHSCORE_UPDATE, pollForHighscores)
}
