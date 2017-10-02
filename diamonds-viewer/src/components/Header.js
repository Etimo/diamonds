import React from 'react';
import styles from '../styles/header.css'
import logo from '../images/Etimo-blue-transparent-nomargins-1000x246.png'

const Header = () => {
  return(
    <div className={styles.header}>
      <a href="https://etimo.se/" target="_blank" rel="noopener"><img className={styles.logo} src={logo}/></a>
      <h1 className={styles.title}>Etimo Diamonds</h1>
      <a className={styles.link} target="_blank" rel="noopener" href="https://github.com/Etimo/diamonds">How to play</a>
    </div>
  );
}

export default Header;
