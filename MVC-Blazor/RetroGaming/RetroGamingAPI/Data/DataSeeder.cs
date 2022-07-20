using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RetroGamingShared.Models;

namespace RetroGamingAPI.Data
{
    public class DataSeeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            context.Database.Migrate();
            if (!userManager.Users.Any())
            {
                // Seed IdentityUsers
                var alice = new IdentityUser("alice") { Email = "AliceSmith@email.com" };
                var result = await userManager.CreateAsync(alice, "Alice123!");
                if (!result.Succeeded)
                    throw new Exception("Could not create user 'alice'");

                var bob = new IdentityUser("bob") { Email = "BobSmith@email.com" };
                result = await userManager.CreateAsync(bob, "Bob1234!");
                if (!result.Succeeded)
                    throw new Exception("Could not create user 'bob'");

                // Seed Platforms Table
                var ps1 = new Platform { Name = "PlayStation 1" };
                var n64 = new Platform { Name = "Nintendo 64" };
                var sd = new Platform { Name = "Sega Dreamcast" };
                var atari2600 = new Platform { Name = "Atari 2600" };
                context.Platforms.AddRange(ps1, n64, sd, atari2600);
                context.SaveChanges();

                // Seed Games Table
                var crash = new Game
                {
                    Title = "Crash Bandicoot",
                    Description = "Crash Bandicoot is a 1996 platform video game developed by Naughty Dog and " +
                            "published by Sony Computer Entertainment for the PlayStation. The game's premise chronicles " +
                            "the creation of the titular Crash, a bandicoot who has been uplifted by the mad scientist Doctor Neo Cortex. " +
                            "The story follows Crash as he aims to prevent Cortex's plans for world domination and rescue " +
                            "his girlfriend Tawna, a female bandicoot also created by Cortex.",
                    PlatformId = ps1.Id
                };
                var zelda = new Game
                {
                    Title = "The Legend of Zelda: Ocarina of Time",
                    Description = "A young Hylian boy Link, must defend Hyrule and the Triforce from the Gerudo King Ganondorf. " +
                        "Through the power of the Ocarina of Time, Link travels back and forth through time to set things right again. " +
                        "Basically a prequel to all Zelda games, it shows Link's original life and trials before all other Zelda games.",
                    PlatformId = n64.Id
                };
                var klonoa = new Game
                {
                    Title = "Klonoa: Door To Phantomile",
                    Description = "Klonoa: Door to Phantomile is a platform game developed and published by Namco for the PlayStation in 1997 " +
                        "and the first game in the Klonoa series. The story follows Klonoa and his friend Huepow in their efforts to save the dream world " +
                        "of Phantomile from an evil spirit intent on turning it into a world of nightmares. The player controls Klonoa through a 2.5D perspective; " +
                        "the stages are rendered in three dimensions but the player moves along a 2D path.",
                    PlatformId = ps1.Id
                };
                var sonic = new Game
                {
                    Title = "Sonic Adventure",
                    Description = "Sonic Adventure is a 1998 platform game for Sega's Dreamcast and the first main Sonic the Hedgehog game to feature 3D gameplay. " +
                    "The story follows Sonic the Hedgehog, Miles Tails Prower, Knuckles the Echidna, Amy Rose, Big the Cat, and E-102 Gamma in their quests to collect " +
                    "the seven Chaos Emeralds and stop series antagonist Doctor Robotnik from unleashing Chaos, an ancient evil.",
                    PlatformId = sd.Id
                };
                var pitfall = new Game
                {
                    Title = "Pitfall II: Lost Caverns",
                    Description = "Pitfall II: Lost Caverns is a platform video game originally released for the Atari 2600 by Activision in 1984. It is the sequel " +
                        "to Pitfall! (1982). Both games were designed and programmed by David Crane and star jungle explorer Pitfall Harry. Pitfall II's major additions " +
                        "are a much larger world with vertical scrolling, swimmable rivers with deadly eels, music, and balloons for floating between locations. ",
                    PlatformId = atari2600.Id
                };
                context.Games.AddRange(crash, zelda, klonoa, sonic, pitfall);
                context.SaveChanges();

                // Seed OwnedGames Table
                var og1 = new OwnedGame() { GameId = crash.Id, OwnerId = alice.Id, StateDescription = "Unopened", Score = 9 };
                var og2 = new OwnedGame() { GameId = crash.Id, OwnerId = bob.Id, StateDescription = "Good", Score = 8 };
                var og3 = new OwnedGame() { GameId = zelda.Id, OwnerId = alice.Id, StateDescription = "Boxed", Score = 10 };
                var og4 = new OwnedGame() { GameId = klonoa.Id, OwnerId = bob.Id, StateDescription = "Manual Missing", Score = 7 };
                var og5 = new OwnedGame() { GameId = sonic.Id, OwnerId = alice.Id, StateDescription = "Poor", Score = 6 };
                var og6 = new OwnedGame() { GameId = sonic.Id, OwnerId = bob.Id, StateDescription = "Ok", Score = 8 };
                var og7 = new OwnedGame() { GameId = pitfall.Id, OwnerId = bob.Id, StateDescription = "New", Score = 8 };
                context.OwnedGames.AddRange(og1, og2, og3, og4, og5, og6, og7);
                context.SaveChanges();
            }

        }
    }
}

