using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RetroGamingAPI.Data;
using RetroGamingShared.Models;

namespace RetroGamingAPI.Services
{
    public class GamesRepository : GamesRepositoryInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        public GamesRepository(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<OwnedGame> GetOwnedGamesFromOwner(string ownerId)
        {
            return _context.OwnedGames
                .Include(og => og.Game)
                .Include(og => og.Game.Platform)
                .Where(og => og.OwnerId == ownerId)
                .ToList();
        }

        public string GetIdFromName(string username)
        {
            //IdentityUser user = new IdentityUser();
            //user.UserName = username;

            IdentityUser user = _context.Users.FirstOrDefault(x => x.UserName == username);
            string userId = user?.Id;

            return userId;

        }

        public OwnedGame GetOwnedGame(int gameId, string ownerId)
        {
            return _context.OwnedGames
                .Include(og => og.Game)
                .Include(og => og.Owner)
                .Include(og => og.Game.Platform)
                .FirstOrDefault(og => (og.GameId == gameId && og.OwnerId == ownerId));
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games
                .Include(x => x.Platform);
        }

        public Game GetGame(int gameId)
        {
            return _context.Games
                .Include(x => x.Platform)
                .FirstOrDefault(x => x.Id == gameId);
        }

        public IEnumerable<Game> GetTopOwnedGames(int topLength = 3)
        {
            var allOwnedGames = _context.OwnedGames
                .Include(og => og.Game)
                .Include(og => og.Owner)
                .Include(og => og.Game.Platform);

            var listCrash = new List<Game>();
            var listZelda = new List<Game>();
            var listKlonoa = new List<Game>();
            var listSonic = new List<Game>();
            var listPitfall = new List<Game>();

            foreach (var ownedgame in allOwnedGames)
            {
                if (ownedgame.Game.Title.Contains("Crash"))
                {
                    listCrash.Add(
                        new Game
                        {
                            Title = ownedgame.Game.Title,
                            Description = ownedgame.Game.Description,
                            Platform = ownedgame.Game.Platform
                        });
                }
                if (ownedgame.Game.Title.Contains("Zelda"))
                {
                    listZelda.Add(
                        new Game
                        {
                            Title = ownedgame.Game.Title,
                            Description = ownedgame.Game.Description,
                            Platform = ownedgame.Game.Platform
                        });
                }
                if (ownedgame.Game.Title.Contains("Klonoa"))
                {
                    listKlonoa.Add(
                        new Game
                        {
                            Title = ownedgame.Game.Title,
                            Description = ownedgame.Game.Description,
                            Platform = ownedgame.Game.Platform
                        });
                }
                if (ownedgame.Game.Title.Contains("Sonic"))
                {
                    listSonic.Add(
                        new Game
                        {
                            Title = ownedgame.Game.Title,
                            Description = ownedgame.Game.Description,
                            Platform = ownedgame.Game.Platform
                        });
                }
                if (ownedgame.Game.Title.Contains("Pitfall"))
                {
                    listPitfall.Add(
                        new Game
                        {
                            Title = ownedgame.Game.Title,
                            Description = ownedgame.Game.Description,
                            Platform = ownedgame.Game.Platform
                        });
                }
            }

            var listOfSeperateGameLists = new List<List<Game>>();
            listOfSeperateGameLists.Add(listCrash);
            listOfSeperateGameLists.Add(listKlonoa);
            listOfSeperateGameLists.Add(listZelda);
            listOfSeperateGameLists.Add(listSonic);
            listOfSeperateGameLists.Add(listPitfall);

            List<Game> mostPopularGames = new List<Game>();
            mostPopularGames.Add(listCrash[0]);
            mostPopularGames.Add(listSonic[0]);
            mostPopularGames.Add(listZelda[0]);


            return mostPopularGames;
        }


        public IEnumerable<Game> GetAllGamesAPI()
        {
            HttpClient client = _httpClientFactory.CreateClient(ApiConstants.RetroGamesApiHttpClientName);
            client.DefaultRequestHeaders.Add("ApiKey", "MyApiKey");
            HttpResponseMessage response = client.GetAsync("games").Result;
            if (response.IsSuccessStatusCode)
            {
                IList<Game> games =
                    response.Content.ReadAsAsync<IList<Game>>().Result;
                return games;
            }
            else
            {
                return Enumerable.Empty<Game>();
            }

        }

        public Game GetGameAPI(int gameId)
        {
            HttpClient client = _httpClientFactory.CreateClient(ApiConstants.RetroGamesApiHttpClientName);
            client.DefaultRequestHeaders.Add("ApiKey", "MyApiKey");
            HttpResponseMessage response = client.GetAsync("games").Result;
            if (response.IsSuccessStatusCode)
            {
                Game game =
                    response.Content.ReadAsAsync<Game>().Result;
                return game;
            }
            else
            {
                return null;
            }
        }
    }
}
