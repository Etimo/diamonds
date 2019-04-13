import React from "react";
import _ from "lodash";
import { connect } from "react-redux";
import styled, { keyframes, css } from "styled-components";
import {
  base,
  botBaseDiamond,
  botBase,
  diamond,
  diamondRed,
  botDiamond,
  robot,
  teleporter,
  wall,
  redButton
} from "../images";

const BoardCellContainer = styled.div`
  border: 1px solid #707070;
  text-align: center;
  flex: 1;
  width: ${props => `${props.cellSize}vw`};
  height: ${props => `${props.cellSize}vw`};
  max-width: 4vw;
  max-height: 4vw;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  @media only screen and (max-width: 900px) {
    max-width: initial;
    max-height: initial;
  }
`;

const spin = keyframes`
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
`;

const CharacterName = styled.p`
  text-overflow: ellipsis;
  white-space: nowrap;
  text-overflow: ellipsis;
  text-align: center;
  font-size: 70%;
  line-height: 1;
  background: rgba(255, 255, 255, 0.8);
  border-radius: 2px;
  padding: 2px;
  margin: 0;

  @media only screen and (max-width: 900px) {
    font-size: 50%;
  }
`;

const CharacterImg = styled.img`
  width: 80%;
  ${props =>
    props.rotate &&
    css`
      animation: ${spin} 10s infinite;
      animation-timing-function: linear;
    `}
`;

const decideCharacter = content => {
  const goImgMap = {
    Teleporter: teleporter,
    Wall: wall,
    DiamondButton: redButton
  };

  if (
    _.has(content, "botName") &&
    _.has(content, "base") &&
    _.has(content, "diamond")
  ) {
    return botBaseDiamond;
  } else if (_.has(content, "botName") && _.has(content, "base")) {
    return botBase;
  } else if (_.has(content, "botName") && _.has(content, "diamond")) {
    return botDiamond;
  } else if (_.has(content, "base")) {
    return base;
  } else if (_.has(content, "botName")) {
    return robot;
  } else if (_.has(content, "diamond")) {
    return content.points === 1 ? diamond : diamondRed;
  } else if (_.has(content, "go")) {
    return goImgMap[content.goName];
  } else {
    return null;
  }
};

const decideCharacterName = content => {
  if (_.has(content, "botName")) {
    return content.botName;
  } else if (_.has(content, "base")) {
    return content.base;
  } else {
    return null;
  }
};

const BoardCell = ({ boardWidth, content }) => {
  const cellSize = Math.floor(100 / boardWidth);

  const character = decideCharacter(content);
  const characterName = decideCharacterName(content);
  const shouldRotate = content.goName === "Teleporter" ? 1 : 0;

  return (
    <BoardCellContainer cellSize={cellSize}>
      {characterName && <CharacterName>{characterName}</CharacterName>}
      {character && (
        <CharacterImg alt="Player" rotate={shouldRotate} src={character} />
      )}
    </BoardCellContainer>
  );
};

const mapStateToProps = ({ board }) => {
  return { boardWidth: board.width };
};

export default connect(mapStateToProps)(BoardCell);
