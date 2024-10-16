using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Wishlist
{
    public int WishlistID { get; set; } // Unique identifier for the wishlist
    public int UserID { get; set; } // ID of the user who owns the wishlist
    public List<Game> WishlistItems { get; set; } = new List<Game>(); // List of games in the wishlist
    public int TotalItems 
    {
        get
        {
            return WishlistItems.Count; // Calculate total number of games in the wishlist
        }
    }
}
