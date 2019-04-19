import React from "react";
import { connect } from "react-redux";
import { updateAllSeasons } from "../actions/updateHighScores";
import Table from "../blocks/Table";

class AllSeasonsTable extends React.Component {
  componentDidMount = () => {
    this.props.updateAllSeasons();
    this.interval = setInterval(this.props.updateAllSeasons, 10000);
  };

  componentWillUnmount = () => {
    clearInterval(this.interval);
  };

  render = () => {
    const {
      highScores: { allSeasons }
    } = this.props;

    return (
      <Table>
        <Table.Caption>All Seasons</Table.Caption>
        <Table.Thead>
          <Table.Tr>
            <Table.Th radiusLeft>Name</Table.Th>
            <Table.Th>Season</Table.Th>
            <Table.Th radiusRight>Score</Table.Th>
          </Table.Tr>
        </Table.Thead>

        <Table.Tbody>
          {allSeasons.map(bot => {
            return (
              <Table.Tr key={bot.botName}>
                <Table.Td>{bot.botName}</Table.Td>
                <Table.Td>{bot.season.name}</Table.Td>
                <Table.Td>{bot.score}</Table.Td>
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
  { updateAllSeasons }
)(AllSeasonsTable);
