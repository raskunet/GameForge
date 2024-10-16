using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;
public class Categories
{
    [Key]
    public int CategoryID { get; set; }

    public string CategoryName {get; set;} = string.Empty;

    public ICollection<Game> Games { get; } = new List<Game>();

    public required User User { get; set; }

    public int NumOfGames {get;set;}

    public int NumOfCategories {get;set;}

    public string Description { get; set; } = string.Empty;
}

public class TrendingGame
{
    [Key]
    public int GameID { get; set; } // Unique identifier for the game
    public string GameName { get; set; }=string.Empty; // Name of the game
    public Categories? Category { get; set; } // Category the game belongs to
    public double Rating { get; set; } // A score representing the game's popularity
    public int NumberOfPlayers { get; set; } // Current number of players
    public DateTime TrendingStartDate { get; set; } // Start date when the game became trending
    public DateTime? TrendingEndDate { get; set; } // Optional end date if the game is no longer trending
    public bool IsCurrentlyTrending { get; set; } // Indicates if the game is still trending
    public required User User { get; set; }
}
