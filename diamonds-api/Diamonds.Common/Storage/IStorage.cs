using System.Collections.Generic;
using System.Threading.Tasks;
using Diamonds.Common.Entities;
using Diamonds.Common.Enums;
using Diamonds.Common.Models;

namespace Diamonds.Common.Storage
{
    public interface IStorage
    {
        Task<Bot> GetBotAsync(BotRegistrationInput input);
        Task<Bot> GetBotAsync(string token);
        Task<Bot> AddBotAsync(BotRegistrationInput input);
        Task UpdateBotAsync(Bot bot);
        Task<IEnumerable<Board>> GetBoardsAsync();
        Task<Board> GetBoardAsync(string id);
        Task UpdateBoardAsync(Board board);
        Task CreateBoardAsync(Board board);

        Task<IEnumerable<Highscore>> GetHighscoresAsync(SeasonSelector season, string botName = null);
        Task SaveHighscoreAsync(Highscore score);
    }
}
