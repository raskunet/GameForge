using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace GameForge.Models;

public class Trending
{
    [Key]
    public int TrendId {get;set;}
    public int GameID { get; set; } // Unique identifier for the game
    public int UserID{get;set;}
    public Game? game{get;set;}
    public DateTime TrendingStartDate { get; set; } // Start date when the game became trending
    public User? User { get; set; }
}
