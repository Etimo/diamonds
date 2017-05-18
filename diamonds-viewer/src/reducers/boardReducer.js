import * as actionTypes from '../constants/actionTypes'
import initialState from './initialState'

const getEmptyGameboard = (width, height) => {
    const gameboard = []
    for (let i = 0; i < width; i++) {
        gameboard.push([])
        for (let j = 0; j < height; j++) {
            gameboard[i][j] = {};
        }
    }

    return gameboard;
}

const boardReducer = (state = initialState.gameboard, action) => {
    switch (action.type) {
        case actionTypes.BOARD_UPDATE_RECEIVED:
            const newGameboard = getEmptyGameboard(action.gameboard.width, action.gameboard.height)

            action.gameboard.bots.map(bot => {
                newGameboard[bot.position.x][bot.position.y] = {
                    ...newGameboard[bot.position.x][bot.position.y],
                    botName: bot.name
                }
                newGameboard[bot.base.x][bot.base.y] = {
                    ...newGameboard[bot.base.x][bot.base.y],
                    base: bot.name
                }
            })
            action.gameboard.diamonds.map(diamond => {
                newGameboard[diamond.x][diamond.y] = {
                    ...newGameboard[diamond.x][diamond.y],
                    diamond: true,
                }
            })
            return {
                ...state,
                bots: action.gameboard.bots,
                rows: newGameboard
            }

        case actionTypes.HIGHSCORE_UPDATE_RECEIVED:
            return {
                ...state,
                highscores: action.highscores
            }

        default:
            return state
    }
}

export default boardReducer