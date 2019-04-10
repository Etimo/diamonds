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
  justify-content: center;
  flex-wrap: wrap;
`;

const Col = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  margin: 1rem;
`;

export default () => {
  return (
    <Container>
      <Row>
        <Board />
        <Col>
          <PlayerList />
          <HighScore />
        </Col>
      </Row>
    </Container>
  );
};
