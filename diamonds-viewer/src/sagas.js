import { delay } from 'redux-saga'
import { put, takeEvery, call } from 'redux-saga/effects'
import axios from 'axios'

import * as actionTypes from './constants/actionTypes'

function* pollForUpdates() {
    //while(true) {
        yield delay(500)
        //const newGameboard = yield call(axios.get, ['/gameboard/1'])
        const newGameboard = {
            bots: [
                {
                    name: 'Bot one',
                    base: {
                        x: 0,
                        y: 0
                    },
                    position: {
                        x: 1,
                        y: 1
                    },
                    score: 1,
                    diamonds: 2,
                    timeLeft: 60*5*1000
                },
                {
                    name: 'Bot two',
                    base: {
                        x: 7,
                        y: 7
                    },
                    position: {
                        x: 9,
                        y: 9
                    },
                    score: 2,
                    diamonds: 5,
                    timeLeft: 15000
                }
            ],
            diamonds: [
                {
                    x: 5,
                    y: 6
                },
                {
                    x: 8,
                    y: 9
                },
                {
                    x: 9,
                    y: 9
                }
            ],
            boardId: 1,
            width: 10,
            height: 10
        }
        yield put({type: actionTypes.BOARD_UPDATE_RECEIVED, gameboard: newGameboard})
    //}
}

export function* updateSaga() {
    yield takeEvery(actionTypes.REQUEST_BOARD_UPDATE, pollForUpdates)
}

function* pollForHighscores() {
    const newHighscores = [{
        name: 'Oscar',
        score: 999
    },
    {
        name: 'Johan',
        score: 99
    },
    {
        name: 'Joakim',
        score: 1
    }]

    yield put({type: actionTypes.HIGHSCORE_UPDATE_RECEIVED, highscores: newHighscores})
}

export function* highscoreSaga() {
    yield takeEvery(actionTypes.REQUEST_HIGHSCORE_UPDATE, pollForHighscores)
}