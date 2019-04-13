import React from "react";
import styled from "styled-components";
import Board from "./components/Board";
import HighScore from "./components/HighScore";
import PlayerList from "./components/PlayerList";

const Container = styled.div`
  margin: 1rem;
`;

const Row = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  flex-wrap: wrap;

  @media only screen and (max-width: 900px) {
    flex-direction: column;
  }
`;

const ScoreContainer = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  min-width: 320px;
  margin: 0 2rem;
`;

export default () => {
  return (
    <Container>
      <Row>
        <Board />
        <ScoreContainer>
          <PlayerList />
          <HighScore />
        </ScoreContainer>
      </Row>
    </Container>
  );
};
