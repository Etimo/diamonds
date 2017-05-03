import React from 'react'
import Cell from './Cell'

const Row = (props) => {
    return ( <tr>
        {props.content.map((content, key) => {
            return (<Cell key={key} content={content} />)
        })}
        </tr>
    )
}

export default Row