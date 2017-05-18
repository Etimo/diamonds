import React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import PlayerList from '../components/PlayerList'
import Board from '../components/Board'
import HighscoreList from '../components/HighscoreList'
import * as actions from '../actions/boardActions'

const style = {
  width: '100%'
}

const App = (props) => {
  const startApp = () => {
          props.actions.requestBoardUpdate()
          props.actions.requestHighscoreUpdate()
  }

  console.log('Rendered app!')
  return (
    <div style={style}>
      <Board rows={props.rows} />
      <button onClick={startApp}>Update</button>
      <PlayerList bots={props.bots} />
      <HighscoreList highscores={props.highscores} />
    </div>
  )
}

const mapStateToProps = (state) => {
  return {
    rows: state.gameboard.rows,
    bots: state.gameboard.bots,
    highscores: state.gameboard.highscores
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    actions: bindActionCreators(actions, dispatch)
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App)
