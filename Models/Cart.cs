using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Cart
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartID { get; set; } // Unique identifier for the cart
    public int UserID { get; set; }
    public int GameId { get; set; }  // List of items in the cart
    public Game? game{get;set;}
    // public decimal TotalPrice 
    // { 
    //     get 
    //     {
    //         return CartItems.Sum(item => item.Price); // Calculate the total price of the cart
    //     }
    // }
    public DateTime CreationDate { get; set; } = DateTime.Now; // Cart creation time
    public bool IsCheckedOut { get; set; } = false; // Flag for checkout status
    public User? User { get; set; }
}

public class CartViewModel
{
    public List<Cart> CartItems { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalCollectables{get;set;}=0;
}

