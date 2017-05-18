import React from 'react'

const HighscoreList = props => (
    <table>
        <thead><tr><th>Name</th><th>Score</th></tr></thead>
        <tbody>
        {props.highscores.map(highscore => (
            <tr key={highscore.name}><td>{highscore.name}</td><td>{highscore.score}</td></tr>
        ))}
        </tbody>
    </table>
)

export default HighscoreList