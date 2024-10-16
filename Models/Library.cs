using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Library
{
    public int LibraryID { get; set; } // Unique identifier for the library
    public int UserID { get; set; } // ID of the user who owns the library
    public List<Game> DownloadedGames { get; set; } = new List<Game>(); // List of downloaded games
    public int TotalGames 
    {
        get
        {
            return DownloadedGames.Count; // Total number of games in the library
        }
    }
    public DateTime LibraryCreationDate { get; set; } = DateTime.Now; // Date the library was created
    public required User User { get; set; }
}
