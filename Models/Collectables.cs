using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Collectables
{
    public int CollectableID { get; set; } // Unique identifier for the collection
    public int UserID { get; set; } // ID of the user who owns the collectables
    public List<Game> CollectableItems { get; set; } = new List<Game>(); // List of collected items
    public int TotalCollectables 
    {
        get
        {
            return CollectableItems.Count * 100; // Total number of collectables
        }
    }
    public string CollectablesDescription { get; set; }=string.Empty; // Optional description of the collection
}
