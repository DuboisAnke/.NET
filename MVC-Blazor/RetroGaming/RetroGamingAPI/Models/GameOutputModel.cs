using RetroGamingShared.Models;

namespace RetroGamingAPI.Models
{
    public class GameOutputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Platform Platform { get; set; }

        public static GameOutputModel FromGameToJSON(Game game)
        {
            return new GameOutputModel()
            {
                Title = game.Title,
                Description = game.Description,
                Platform = game.Platform
            };
        }
    }
}
