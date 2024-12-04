using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Wallet
{
    public int ID { get; set; } // Unique identifier for the collection
    public string? UserID { get; set; } // ID of the user who owns the collectables
    public decimal TotalAmount {get;set;}=0;
}
