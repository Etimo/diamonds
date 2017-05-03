import * as types from '../constants/actionTypes'

export const requestBoardUpdate = () => {
    console.log('Reached action creator!')
    return {
        type: types.REQUEST_BOARD_UPDATE
    }
}