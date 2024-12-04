using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace GameForge.Models;

public class Featured
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}
    public int GameID { get; set; } // Unique identifier for the game
    public string? UserID{get;set;}
    public Game? game{get;set;}
    public DateTime FeaturingStartDate { get; set; } // Start date when the game became trending
    public User? User { get; set; }
}
