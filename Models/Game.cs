using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Game
{
    public int GameID { get; set; } // Unique identifier for the game
    public int UserID { get; set; }
    public string GameName { get; set; }=string.Empty; // Name of the game
    public string Developer { get; set; }=string.Empty; // Developer of the game
    public string Publisher { get; set; }=string.Empty; // Publisher of the game
    public DateTime ReleaseDate { get; set; } // Release date of the game
    public string Category { get; set; } =string.Empty; // Genre of the game (e.g., RPG, Shooter)
    public decimal Price { get; set; } // Price of the game
    public string Platform { get; set; } =string.Empty;// Platform (e.g., PC, PlayStation)
    public double? Rating { get; set; } // User rating (optional)
}
