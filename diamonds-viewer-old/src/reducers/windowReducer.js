import * as actionTypes from '../constants/actionTypes'
import initialState from './initialState'

const windowReducer = (state = initialState.windowSize, action) => {
  switch(action.type){
    
    case actionTypes.CHANGE_WINDOW_SIZE:
    return{
      ...state,
      width: action.windowSize.width,
      height: action.windowSize.height,
    }
    default:
    return state
  }
}

export default windowReducer;