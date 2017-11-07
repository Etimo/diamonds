import * as actionTypes from '../constants/actionTypes'
import initialState from './initialState'

// const apiUrl = "http://localhost:4000/bots";

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
            const newGameboard = getEmptyGameboard(action.gameboard.height, action.gameboard.width)
            action.gameboard.bots.forEach(bot => {
                newGameboard[bot.position.y][bot.position.x] = {
                    ...newGameboard[bot.position.y][bot.position.x],
                    botName: bot.name
                }
                newGameboard[bot.base.y][bot.base.x] = {
                    ...newGameboard[bot.base.y][bot.base.x],
                    base: bot.name
                }
            })
            action.gameboard.diamonds.forEach(diamond => {
                newGameboard[diamond.y][diamond.x] = {
                    ...newGameboard[diamond.y][diamond.x],
                    diamond: true,
                }
            })
            action.gameboard.gameObjects.forEach(go => {
                newGameboard[go.y][go.x] = {
                    ...newGameboard[go.y][go.x],
                    goName:go.Name,
                    go: true,
                }
            })
            return {
                ...state,
                bots: action.gameboard.bots,
                rows: newGameboard,
                width: action.gameboard.width,
                height: action.gameboard.height
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
