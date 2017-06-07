import React from 'react'
import styles from '../styles/table.css';
import playerlist from '../styles/playerlist.css';
import imgDiamond from '../images/diamond.svg'

const PlayerList = props => (
    <table className={styles.table}>
      <caption className={playerlist.caption}>Players playing now</caption>
      <thead>
        <tr>
          <th className={styles.thRadiusLeft}>Name</th>
          <th>Diamonds</th>
          <th>Score</th>
          <th className={styles.thRadiusRight}>Time</th>
        </tr>
      </thead>
      <tbody>
        { props.bots.map(bot => (
            <tr key={bot.name}>
              <td> { bot.name }</td>
              <td>{[...Array(bot.diamonds)].map(() => <img className={playerlist.diamond} src={imgDiamond}/>)}</td>

              <td> { bot.score } points</td>
              <td>{ bot.timeLeft / 1000 }</td>
            </tr>
        )) }

      </tbody>
    </table>
)

export default PlayerList
