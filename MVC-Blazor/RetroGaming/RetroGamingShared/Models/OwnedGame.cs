using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RetroGamingShared.Models
{
    public class OwnedGame
    {   
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Models.Game))]
        public int GameId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public IdentityUser Owner { get; set; }

        public Game Game { get; set; }

        public string StateDescription { get; set; }

        [Range(0, 10)]
        public int Score { get; set; }
    }
}
