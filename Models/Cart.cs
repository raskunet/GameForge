using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartID { get; set; } // Unique identifier for the cart
    public string? UserID { get; set; }
    public int GameId { get; set; }  // List of items in the cart
    public Game? game{get;set;}
    public DateTime CreationDate { get; set; } = DateTime.Now; // Cart creation time
    public bool IsCheckedOut { get; set; } = false; // Flag for checkout status
    public User? User { get; set; }
}

public class CartViewModel
{
    public List<Cart>? CartItems { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalCollectables{get;set;}=0;
}

