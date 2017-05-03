import React from 'react'
import Row from './Row'

const Board = (props) => {
    return ( <table><tbody>
        {props.rows.map((content, key) => {
            return (<Row key={key} content={content} />)
        })}
        </tbody></table>
    )
}

export default Board