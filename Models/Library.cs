using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace GameForge.Models;

public class Library
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LibraryID {get;set;}
    public string? UserID { get; set; } // ID of the user who owns the collectables
    public int GameId {get;set;}
    public Game? game{get;set;}
    public DateTime LibraryCreationDate { get; set; } = DateTime.Now; // Date the library was created
    public User? User { get; set; }
}

public class LibraryViewModel
{
    public List<Library>? Items { get; set; }
    public decimal TotalCollectables{get;set;}=0;
}
