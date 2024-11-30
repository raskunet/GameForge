using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

[PrimaryKey("GameProblemID")]
public class GameProblem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GameProblemID { get; set; }
    public int UserID { get; set; }
    public int GameID { get; set; }
    public required string Title { get; set; }
    public required string ProblemDescription { get; set; }
    public DateTime CreationDate{ get; set; }
    public required User User { get; set; }
    public required Game Game { get; set; }
}

[NotMapped]
public class GameProblemCreateVM
{
    public int UserID { get; set; }
    public int GameID { get; set; }
    public string ProblemTitle { get; set; } = null!;
    public string ProblemDescription { get; set; } = null!;
}

[NotMapped]
public class GameProblemsIndex
{
    public List<GameProblem> GameProblems { get; set; } = null!;
}