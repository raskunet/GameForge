using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

[PrimaryKey("QuestionID","AuthorID")]
public class Question
{
    public int QuestionID { get; set; }
    public int AuthorID { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public int LatestAnswerID { get; set; }
    public DateTime LatestAnswerTime { get; set; }
    public int NumberOfAnswers { get; set; }
    public required User User { get; set; }

    public ICollection<Answer> Answers { get; } = new List<Answer>();

}

[NotMapped]
public class QuestionPost
{
    public required Question Question { get; set; }
    public List<Answer> Answers { get; } = new List<Answer>();
}