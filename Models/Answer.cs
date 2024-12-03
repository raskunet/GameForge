using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace GameForge.Models;

[PrimaryKey("QuestionID", "UserID")]
public class Answer
{
    public int QuestionID { get; set; }
    public string UserID { get; set; } = null!;
    public string AnswerText { get; set; } = string.Empty;
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastEditTime;
    public required User User { get; set; }
    public required Question Question { get; set; }
}

[NotMapped]
public class AnswerCreateViewModel
{
    public string AnswerText { get; set; } = null!;
    [HiddenInput(DisplayValue = false)]
    public int QuestionID { get; set; }
    public bool CanCreate { get; set; } = true;
}

[NotMapped]
public class AnswerVoteAction
{
    public int QuestionID { get; set; }
    public required string UserID { get; set; }
    public bool Type { get; set; }
}

[NotMapped]
public class AnswerEditViewModel
{
    public required int QuestionID { get; set; }

    public required string UserID { get; set; }
    public required string AnswerText { get; set; }
    public bool CanEdit { get; set; } = true;
}