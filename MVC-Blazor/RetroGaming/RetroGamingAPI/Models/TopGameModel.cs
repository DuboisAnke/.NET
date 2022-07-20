using RetroGamingShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGamingAPI.Models
{
    public class TopGameModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PlatformName { get; set; }

        public static TopGameModel FromGame(Game game)
        {
            return new TopGameModel()
            {
                Title = game.Title,
                Description = game.Description,
                PlatformName = game.Platform.Name
            };
        }
    }
}
