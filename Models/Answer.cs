using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace GameForge.Models;

[PrimaryKey("QuestionID","UserID")]
public class Answer
{
    public int QuestionID { get; set; }
    public int UserID { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastEditTime;
    public required User User { get; set; }
    public required Question Question { get; set; }
}