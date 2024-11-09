using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

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

[NotMapped]
public class AnswerCreateViewModel
{
    public string AnswerText { get; set; } = null!;
    [HiddenInput(DisplayValue =false)]
    public int QuestionID{ get; set; }
}