using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroGamingAPI.Attributes;
using RetroGamingAPI.Models;
using RetroGamingAPI.Services;
using RetroGamingShared.Models;

namespace RetroGamingAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class GamesController : ControllerBase
    {
        private readonly GamesRepositoryInterface _gameRepository;


        public GamesController(GamesRepositoryInterface gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int id)
        {
            Game game = _gameRepository.GetGameAPI(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(GameOutputModel.FromGameToJSON(game));
        }

        public IActionResult GetAllGames()
        {
            try
            {
                IEnumerable<Game> games = _gameRepository.GetAllGamesAPI();
                List<GameOutputModel> models = games.Select(x => GameOutputModel.FromGameToJSON(x)).ToList();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
