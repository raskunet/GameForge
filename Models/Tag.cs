using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameForge.Models;

public class ThreadTag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ThreadTagID{ get; set; }
    public required string TagName{ get; set; }
    public DateTime CreationDate { get; set; }
}