import * as actionTypes from '../constants/actionTypes'

export const changeWindowSize = () => {
    return{
        type: actionTypes.CHANGE_WINDOW_SIZE,
        windowSize: {
            width: window.innerWidth,
            height: window.innerHeight
        }

    }
}
