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
  justify-content: ${props =>
    props.spaceBetween ? "space-between" : "center"};
  flex-wrap: ${props => (props.nowrap ? "nowrap" : "wrap")};
`;

const Col = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  margin: 1rem;
`;

export default () => {
  return (
    <Container>
      <Col>
        <Row nowrap spaceBetween>
          <h2>Diamonds</h2>
        </Row>
        <Row>
          <Board />
          <Col>
            <PlayerList />
            <HighScore />
          </Col>
        </Row>
      </Col>
    </Container>
  );
};
