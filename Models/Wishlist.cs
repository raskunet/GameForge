using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Wishlist
{
    public int WishlistID { get; set; } // Unique identifier for the wishlist
    public string? UserID { get; set; } // ID of the user who owns the wishlist
    public int GameId {get;set;}
    public Game? game{get;set;}
    public DateTime WishlistAdditionDate { get; set; } = DateTime.Now; // Date the library was created
    public User? User { get; set; }
    
}


public class WishlistViewModel
{
    public List<Wishlist>? Wishlist { get; set; }
    
}