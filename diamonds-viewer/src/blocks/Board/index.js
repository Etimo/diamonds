import styled from "styled-components";
import Row from "./Row";
import Cell from "./Cell";
import CharacterName from "./CharacterName";
import CharacterImg from "./CharacterImg";

const Board = styled.div`
  border: 1px solid #333;
  border-radius: 4px;
  display: flex;
  flex-direction: column;
  margin-bottom: 1.5rem;
`;

Board.Row = Row;
Board.Cell = Cell;
Board.CharacterName = CharacterName;
Board.CharacterImg = CharacterImg;

export default Board;
