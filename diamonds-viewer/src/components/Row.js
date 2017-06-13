import React from 'react'
import Cell from './Cell'
import styles from '../styles/row.css'

const Row = (props) => {
    return ( 
        <div className={styles.row}>
            {props.content.map((content, key) => {
                return (<Cell key={key} content={content}/>)
            })}
        </div>
    )
}

export default Row
