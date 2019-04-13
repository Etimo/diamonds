import React from "react";
import { connect } from "react-redux";
import _ from "lodash";
import styled from "styled-components";
import { diamond } from "../images";

const Diamond = styled.img`
  height: 50%;
`;

const Table = styled.table`
  width: 100%;
`;

const Caption = styled.caption`
  font-weight: bold;
  margin-bottom: 10px;
  text-align: left;
  margin-left: 10px;

  @media only screen and (min-width: 1600px) {
    font-size: 1.5rem;
  }
`;

const Tr = styled.tr`
  border-bottom: 1px solid #adadad;
`;

const Th = styled.th`
  padding: 10px;
  text-align: left;
  font-size: 0.8em;
  font-weight: 600;
  background-color: #ecf0f1;
  color: #3d3d3d;
`;

const PlayerList = ({ bots }) => {
  return (
    <Table>
      <Caption>Active Players</Caption>
      <thead>
        <Tr>
          <Th>Name</Th>
          <Th>Diamonds</Th>
          <Th>Score</Th>
          <Th>Time</Th>
        </Tr>
      </thead>
      <tbody>
        {bots.map(bot => {
          return (
            <Tr key={bot.name}>
              <td>{bot.name}</td>
              <td>
                {_.times(bot.diamonds, index => {
                  return <Diamond key={index} alt="diamond" src={diamond} />;
                })}
              </td>
              <td>{bot.score}</td>
              <td>{Math.round(bot.millisecondsLeft / 1000)}s</td>
            </Tr>
          );
        })}
      </tbody>
    </Table>
  );
};

const mapStateToProps = ({ bots }) => {
  return { bots };
};

export default connect(mapStateToProps)(PlayerList);
