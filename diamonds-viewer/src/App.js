import React from "react";
import { createGlobalStyle } from "styled-components";

import Layout from "./blocks/Layout";
import GameBoard from "./components/GameBoard";
import PlayerTable from "./components/PlayerTable";
import HighScoreTable from "./components/HighScoreTable";
import AllSeasonsTable from "./components/AllSeasonsTable";

const GlobalStyle = createGlobalStyle`
  html, body {
    height: 100%;
    margin: 0;
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
      <Layout>
        <Layout.Game>
          <GameBoard />
          <Layout.Tables>
            <PlayerTable />
            <HighScoreTable />
            <AllSeasonsTable />
          </Layout.Tables>
        </Layout.Game>
      </Layout>
    </React.Fragment>
  );
};
