import * as types from '../constants/actionTypes'
import initialState from './initialState'

const boardReducer = (state = initialState.rows, action) => {
    switch (action.type) {
        case types.BOARD_UPDATE_RECEIVED:
            return action.board
        default:
            return state
    }
}

export default boardReducer