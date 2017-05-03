import React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import Board from '../components/Board'

import * as actions from '../actions/boardActions'

const App = (props) => {
  console.log('Rendered app!')
  return (
    <div>
      <Board rows={props.rows} />
      <button onClick={() => props.actions.requestBoardUpdate()}>Update</button>
    </div>
  )
}

const mapStateToProps = (state) => {
  return {
    rows: state.rows
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
