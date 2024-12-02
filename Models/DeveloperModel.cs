using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameForge.Models
{
    public class Developer : User
    {
        // List of games created by the developer
        public ICollection<Game> CreatedGames { get; set; } = new List<Game>();
        public ICollection<GameProblem> GameProblems{get;set;}=new List<GameProblem>();

        // Additional developer-specific properties could go here
    }
}
