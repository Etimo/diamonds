import React from 'react'
import Row from './Row'

const style = {
    tableLayout: 'fixed',
    borderCollapse: 'collapse'
}

const Board = (props) => {
    return ( <table style={style} ><tbody>
        {props.rows.map((content, key) => {
            return (<Row key={key} content={content} />)
        })}
        </tbody></table>
    )
}

export default Board