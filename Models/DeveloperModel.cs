using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameForge.Models
{
    public class Developer : User
    {
        // List of games created by the developer
        public ICollection<Game> CreatedGames { get; set; } = new List<Game>();

        // Additional developer-specific properties could go here
    }
}
