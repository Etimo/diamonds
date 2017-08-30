using System.Collections.Generic;
using Diamonds.Common.Entities;
using Diamonds.Common.Models;

namespace Diamonds.Common.Storage
{
    public interface IStorage
    {
        Bot GetBot(BotRegistrationInput input);
        Bot GetBot(string token);
        Bot AddBot(BotRegistrationInput input);
        void UpdateBot(Bot bot);
        IEnumerable<Board> GetBoards();
        Board GetBoard(string id);
        void UpdateBoard(Board board);
        void CreateBoard(Board board);

        IEnumerable<Highscore> GetHighscores();
    }
}
