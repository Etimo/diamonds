import React from 'react'
import Row from './Row'
import styles from '../styles/board.css'

const Board = (props) => {

    return (
      <div className={styles.board}>
      {
        props.rows.map((content, key) => {

          return (<Row key={key} content={content}/>)
        })
      }
      </div>
    )
}

export default Board;
