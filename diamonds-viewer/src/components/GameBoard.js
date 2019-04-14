import React from "react";
import { connect } from "react-redux";
import _ from "lodash";
import updateBoard from "../actions/updateBoard";
import Board from "../blocks/Board";
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

class GameBoard extends React.Component {
  componentDidMount = () => {
    this.interval = setInterval(this.shouldUpdateBoard, 250);
  };

  componentWillUnmount = () => {
    clearInterval(this.interval);
  };

  shouldUpdateBoard = () => {
    const { fetching } = this.props.board;
    const boardId = 1;

    if (!fetching) {
      this.props.updateBoard(boardId);
    }
  };

  decideCharacter = content => {
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

  decideCharacterName = content => {
    if (_.has(content, "botName")) {
      return content.botName;
    } else if (_.has(content, "base")) {
      return content.base;
    } else {
      return null;
    }
  };

  render = () => {
    const { rows, width } = this.props.board;
    const bigCellSize = (100 / width).toFixed(2);
    const smallCellSize = (65 / width).toFixed(2);

    return (
      <Board>
        {rows.map((cells, key) => {
          return (
            <Board.Row key={key}>
              {cells.map((content, key) => {
                const character = this.decideCharacter(content);
                const characterName = this.decideCharacterName(content);
                const shouldRotate = content.goName === "Teleporter" ? 1 : 0;

                return (
                  <Board.Cell
                    key={key}
                    bigCellSize={bigCellSize}
                    smallCellSize={smallCellSize}
                  >
                    {characterName && (
                      <Board.CharacterName>{characterName}</Board.CharacterName>
                    )}
                    {character && (
                      <Board.CharacterImg
                        alt="player"
                        src={character}
                        rotate={shouldRotate}
                      />
                    )}
                  </Board.Cell>
                );
              })}
            </Board.Row>
          );
        })}
      </Board>
    );
  };
}

const mapStateToProps = ({ board }) => {
  return { board };
};

export default connect(
  mapStateToProps,
  { updateBoard }
)(GameBoard);
