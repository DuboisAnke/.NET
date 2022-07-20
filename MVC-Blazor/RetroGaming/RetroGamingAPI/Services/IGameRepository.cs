using RetroGamingShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingAPI.Services
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAllGames();
        Game GetGame(int gameId);
        IEnumerable<Game> GetTopOwnedGames(int topLength = 3);
        IEnumerable<OwnedGame> GetOwnedGamesFromOwner(string ownerId);
        OwnedGame GetOwnedGame(int gameId, string ownerId);

        void AddGameFromOwner(OwnedGame ownedGame);
    }
}
