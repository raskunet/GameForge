using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

public class Collectables
{
    public int CollectableID { get; set; } // Unique identifier for the collection
    public int UserID { get; set; } // ID of the user who owns the collectables
    public int TotalCollectables {get;set;}=0;
    public int LibraryID{get;set;}
    public Library? library{get;set;}
}