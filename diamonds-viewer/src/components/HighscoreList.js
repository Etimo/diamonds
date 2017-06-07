import React from 'react'
import styles from '../styles/table.css';


const HighscoreList = props => (
    <table className={styles.table}>
      <caption>Highscore list</caption>
        <thead>
          <tr>
            <th className={styles.thRadiusLeft}>Name</th>
            <th className={styles.thRadiusRight}>Score</th>
          </tr>
        </thead>
        <tbody>
        {props.highscores.map(highscore => (
            <tr key={highscore.name}>
              <td>{highscore.name}</td>
              <td >{highscore.score}</td>
            </tr>
        ))}
        </tbody>
    </table>
)

export default HighscoreList
