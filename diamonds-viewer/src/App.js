import React from "react";
import { createGlobalStyle } from "styled-components";

import Game from "./Game";

const GlobalStyle = createGlobalStyle`
  html, body {
    height: 100%;
  }
  html {
    box-sizing: border-box;
  }
  *, *:before, *:after {
    box-sizing: inherit;
    font-family: 'PT Sans', sans-serif;
    color: #2C3E50;
  }
`;

export default () => {
  return (
    <React.Fragment>
      <GlobalStyle />
      <Game />
    </React.Fragment>
  );
};
