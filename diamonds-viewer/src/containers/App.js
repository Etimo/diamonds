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

  // startApp(){
  //   this.props.actions.requestBoardUpdate();
  //   this.props.actions.requestHighscoreUpdate();
  // }

  render(){
    return (
      <div>
        <Header/>
        <div className={styles.container}>
          <Board rows={this.props.rows} />

          <div className={styles.info}>
            {/*<button className={styles.btn} onClick={this.startApp}>Update</button>*/}
            <PlayerList bots={this.props.bots} />
            <HighscoreList highscores={this.props.highscores} />
          </div>
        </div>
      </div>
    );
  }

}
// const App = (props) => {
//   const startApp = () => {
//           props.actions.requestBoardUpdate()
//           props.actions.requestHighscoreUpdate()
//   }
//
//   console.log('Rendered app!')
//   return (
//     <div>
//       <Header/>
//       <div className={styles.container}>
//         <Board rows={props.rows} />
//
//         <div className={styles.info}>
//           <button className={styles.btn} onClick={startApp}>Update</button>
//           <PlayerList bots={props.bots} />
//           <HighscoreList highscores={props.highscores} />
//         </div>
//       </div>
//     </div>
//   )
// }

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
