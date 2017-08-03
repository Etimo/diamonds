import React from 'react'
import playerlist from '../styles/playerlist.css';
import styles from '../styles/table.css';
import imgDiamond from '../images/diamond.svg' // eslint-disable-line no-unused-vars

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
        { props.bots.map((bot, num) => (
          
            <tr key={bot.name}>
              <td> { bot.name }</td>
              <td>{'*'.repeat(bot.diamonds)}</td>
              <td> { bot.score }</td>
              <td>{ bot.timeLeft / 1000 }</td>
            </tr>
        )) }

      </tbody>
    </table>
)

export default PlayerList
