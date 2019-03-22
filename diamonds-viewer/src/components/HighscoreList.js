import React from 'react'
import styles from '../styles/table.css';


const HighscoreList = props => (
    <table className={styles.table}>
      <caption>{props.caption || "Highscore"}</caption>
        <thead>
          <tr>
            <th className={styles.thRadiusLeft}>Name</th>
            {props.showSeason && <th>Season</th>}
            <th className={styles.thRadiusRight}>Score</th>
          </tr>
        </thead>
        <tbody>
        {props.highscores.map(highscore => (
            <tr key={highscore.name}>
              <td>{highscore.botName}</td>
              {props.showSeason && <td>{highscore.season.name}</td>}
              <td>{highscore.score}</td>
            </tr>
        ))}
        </tbody>
    </table>
)

export default HighscoreList
