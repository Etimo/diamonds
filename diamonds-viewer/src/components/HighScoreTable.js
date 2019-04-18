import React from "react";
import { connect } from "react-redux";
import { updateCurrentSeason } from "../actions/updateHighScores";
import Table from "../blocks/Table";

class HighScoreTable extends React.Component {
  componentDidMount = () => {
    this.props.updateCurrentSeason();
    this.interval = setInterval(this.props.updateCurrentSeason, 5000);
  };

  componentWillUnmount = () => {
    clearInterval(this.interval);
  };

  render = () => {
    const {
      highScores: { currentSeason }
    } = this.props;

    return (
      <Table>
        <Table.Caption>Highscore</Table.Caption>
        <Table.Thead>
          <Table.Tr>
            <Table.Th radiusLeft width={70}>
              Name
            </Table.Th>
            <Table.Th radiusRight>Score</Table.Th>
          </Table.Tr>
        </Table.Thead>

        <Table.Tbody>
          {currentSeason.map(bot => {
            return (
              <Table.Tr key={bot.botName}>
                <Table.Td>{bot.botName}</Table.Td>
                <Table.Td textRight>{bot.score}</Table.Td>
              </Table.Tr>
            );
          })}
        </Table.Tbody>
      </Table>
    );
  };
}

const mapStateToProps = ({ highScores }) => {
  return { highScores };
};

export default connect(
  mapStateToProps,
  { updateCurrentSeason }
)(HighScoreTable);
