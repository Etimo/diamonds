import React from 'react'
import styles from '../styles/cell.css'
import gameObjecStyles from '../styles/gameobject.css'
import imgDiamond from '../images/diamond.svg'
import imgDiamondRed from '../images/diamondred.svg'
import imgBase from '../images/home.svg'
import imgRobot from '../images/robot.svg'
import imgBotBase from '../images/botBase.svg'
import imgBotDiamond from '../images/botDiamond.svg'
import imgBaseDiamond from '../images/baseDiamond.svg'
import imgBotBaseDiamond from '../images/botBaseDiamond.svg'
import imgTeleporter from '../images/Teleporter.svg'
import imgWall from '../images/wall.svg'
import imgButton from '../images/redButton.svg'
import { connect } from 'react-redux'

const Cell = (props) => {

    const c = props.content;
    const goImgMap = {
        "Teleporter":imgTeleporter,
        "Wall":imgWall,
        "DiamondButton":imgButton
    };

    const character = c.botName && c.diamond && c.base ? imgBotBaseDiamond
    : c.base && c.botName ? imgBotBase
    : c.botName && c.diamond ? imgBotDiamond
    : c.base && c.diamond ? imgBaseDiamond
    : c.base ? imgBase
    : c.botName ? imgRobot
    : c.diamond ? (c.points === 1 ? imgDiamond : imgDiamondRed)
    : c.botName && c.go ? imgRobot
    : c.go ? goImgMap[c.goName]
    : ''

    let image = null;
    let name = null;
    // let base = null;

    if(character){
      image = <img alt="Player" className={styles.cellImg+(c.goName && gameObjecStyles[c.goName]? " "+gameObjecStyles[c.goName] : "") }  src={character}/>;
    }
    
    name = c.botName ? <p className={styles.cellSign}>{c.botName}</p>
    : c.base ? <p className={styles.cellSign}>{c.base}</p>
    : ''

    return (
        <div className={`${styles.cell} ${props.windowSize.width/props.windowSize.height > 2 ? styles.cellHeight: null}`} >
          {name}
          {image}
          
        </div>
    )
}

const mapStateToProps = (state) => {
  return {
    windowSize: state.windowSize,
    boardWidth: state.gameboard.width,
    boardHeight: state.gameboard.height,
  }
}

export default connect(mapStateToProps)(Cell)
