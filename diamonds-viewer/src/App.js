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
