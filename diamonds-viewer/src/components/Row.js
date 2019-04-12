import React from "react";
import styled from "styled-components";
import Cell from "./Cell";

const Row = styled.div`
  width: 100%;
  display: flex;
  flex-direction: row;
  flex: 1;
  flex-basis: fill;
`;

export default ({ content }) => {
  const cells = content.map((cell, key) => <Cell key={key} content={cell} />);
  return <Row>{cells}</Row>;
};
