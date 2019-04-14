import React from "react";
import styled from "styled-components";
import GameBoard from "./components/GameBoard";
import HighScore from "./components/HighScore";
import PlayerList from "./components/PlayerList";

const Container = styled.div`
  margin: 1rem;
`;

const Row = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  flex-wrap: nowrap;

  @media only screen and (max-width: 900px) {
    flex-direction: column;
  }
`;

const ScoreContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 30%;

  @media only screen and (max-width: 900px) {
    width: 100%;
  }
`;

export default () => {
  return (
    <Container>
      <Row>
        <GameBoard />
        <ScoreContainer>
          <PlayerList />
          <HighScore />
        </ScoreContainer>
      </Row>
    </Container>
  );
};
