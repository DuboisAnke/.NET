using System.Collections.Generic;
using RetroGamingShared.Models;

namespace RetroGamingAPI.Services
{
    public interface GamesRepositoryInterface
    {
        IEnumerable<Game> GetAllGames();
        Game GetGame(int gameId);
        IEnumerable<Game> GetTopOwnedGames(int topLength = 3);
        IEnumerable<OwnedGame> GetOwnedGamesFromOwner(string ownerId);
        OwnedGame GetOwnedGame(int gameId, string ownerId);
        string GetIdFromName(string username);
        IEnumerable<Game> GetAllGamesAPI();
        Game GetGameAPI(int gameId);


    }
}
