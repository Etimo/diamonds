import React from "react";
import styled from "styled-components";
import { connect } from "react-redux";
import updateBoard from "../actions/updateBoard";
import BoardRow from "./BoardRow";

const BoardContainer = styled.div`
  border: 1px solid #333;
  border-radius: 4px;
  display: flex;
  flex-direction: column;
`;

class Board extends React.Component {
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

  renderRows = () => {
    return this.props.board.rows.map((cells, key) => (
      <BoardRow key={key} cells={cells} />
    ));
  };

  render = () => {
    return <BoardContainer>{this.renderRows()}</BoardContainer>;
  };
}

const mapStateToProps = ({ board }) => {
  return { board };
};

export default connect(
  mapStateToProps,
  { updateBoard }
)(Board);
