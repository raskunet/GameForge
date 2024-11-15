using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Library
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LibraryID { get; set; } // Auto-incremented ID
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
