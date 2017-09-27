import React, {Component} from 'react';

import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import PlayerList from '../components/PlayerList'
import Board from '../components/Board'
import HighscoreList from '../components/HighscoreList'
import * as actions from '../actions/boardActions'
import styles from '../styles/style.css'
import Header from '../components/Header'


class App extends Component{

  componentWillMount(){
    this.props.actions.requestBoardUpdate();
    this.props.actions.requestHighscoreUpdate();
  }


  render(){
    return (
      <div className={styles.container}>
        <Header/>
        <div className={styles.gameContainer}>
          <Board rows={this.props.rows} />

          <div className={styles.info}>
            <PlayerList bots={this.props.bots} />
            <HighscoreList highscores={this.props.highscores} />
          </div>
        </div>
      </div>
    );
  }

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
