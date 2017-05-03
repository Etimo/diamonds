import { delay } from 'redux-saga'
import { put, takeEvery } from 'redux-saga/effects'

import * as types from '../constants/actionTypes'

function* pollForUpdates() {
    while(true) {
        yield delay(500)
        yield put({type: types.BOARD_UPDATE_RECEIVED})
    }
}

export function* updateSaga() {
    yield takeEvery(types.REQUEST_BOARD_UPDATE, pollForUpdates)
}