import React from "react";
import styled from "styled-components";

const Container = styled.div`
  min-width: 800px;
  border: solid red 1px;
  margin: 1rem;

  @media (max-width: 820px) {
    min-width: initial;
    width: 100%;
  }
`;

class Board extends React.Component {
  render = () => {
    return <Container>Board</Container>;
  };
}

export default Board;
