using Microsoft.EntityFrameworkCore;
using RetroGamingAPI.Data;
using RetroGamingShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingAPI.Services
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext context;

        public GameRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<OwnedGame> GetOwnedGamesFromOwner(string ownerId)
        {
            return context.OwnedGames
                .Include(og => og.Game)
                .Include(og => og.Game.Platform)
                .Where(og => og.OwnerId == ownerId)
                .ToList();
        }

        public OwnedGame GetOwnedGame(int gameId, string ownerId)
        {
            return context.OwnedGames
                    .Include(og => og.Game)
                    .Include(og => og.Owner)
                    .FirstOrDefault(og => (og.GameId == gameId && og.OwnerId == ownerId));   
        }

        public IEnumerable<Game> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Game GetGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetTopOwnedGames(int topLength = 3)
        {
            throw new NotImplementedException();
        }

        public void AddGameFromOwner(OwnedGame ownedGame)
        {
            throw new NotImplementedException();
        }
    }
}
