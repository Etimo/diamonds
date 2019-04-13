import React from "react";
import styled from "styled-components";
import BoardCell from "./BoardCell";

const BoardRowContainer = styled.div`
  width: 100%;
  display: flex;
  flex-direction: row;
  flex: 1;
  flex-basis: fill;
`;

export default ({ cells }) => {
  const renderCells = cells.map((cell, key) => (
    <BoardCell key={key} content={cell} />
  ));
  return <BoardRowContainer>{renderCells}</BoardRowContainer>;
};
