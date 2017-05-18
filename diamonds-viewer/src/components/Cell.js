import React from 'react'

const style = {
    border: '1px solid black',
    height: '1em',
    width: '1em',
    textAlign: 'center'
}

const Cell = (props) => {

    const c = props.content
    
    const character = c.botName && c.diamond && c.base ? 'A'
    : c.base && c.botName ? 'H'
    : c.botName && c.diamond ? 'X'
    : c.base && c.diamond ? '?'
    : c.base ? 'B'
    : c.botName ? 'P'
    : c.diamond ? 'D'
    : ' ';

    return (
        <td style={style}>{character}</td>
    )
}

export default Cell