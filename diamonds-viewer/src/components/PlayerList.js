import React from 'react'

const PlayerList = props => (
    <div>
        { props.bots.map(bot => (
            <div key={bot.name}>
                { '*'.repeat(bot.diamonds) } { bot.name }: { bot.score } points - Time: { bot.timeLeft / 1000 }
            </div>
        )) }
    </div>
)

export default PlayerList