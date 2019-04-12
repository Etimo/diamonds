import React from "react";
import _ from "lodash";
import { connect } from "react-redux";
import styled from "styled-components";
import {
  base,
  botBaseDiamond,
  botBase,
  diamond,
  diamondRed,
  botDiamond,
  robot
} from "../images";

const CellContainer = styled.div`
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

const CharacterImg = styled.img`
  width: 80%;
`;

class Cell extends React.Component {
  character = () => {
    const { content } = this.props;

    if (_.has(content, ["botName", "base", "diamond"])) {
      return botBaseDiamond;
    } else if (_.has(content, ["botName", "base"])) {
      return botBase;
    } else if (_.has(content, ["botName", "diamond"])) {
      return botDiamond;
    } else if (_.has(content, "base")) {
      return base;
    } else if (_.has(content, "botName")) {
      return robot;
    } else if (_.has(content, "diamond")) {
      return content.points === 1 ? diamond : diamondRed;
    } else {
      return null;
    }
  };

  render = () => {
    const { boardWidth } = this.props;
    const cellSize = Math.floor(100 / boardWidth);

    const character = this.character();

    return (
      <CellContainer cellSize={cellSize}>
        {character && <CharacterImg alt="Player" src={character} />}
      </CellContainer>
    );
  };
}

const mapStateToProps = ({ board }) => {
  return { boardWidth: board.width };
};

export default connect(mapStateToProps)(Cell);
