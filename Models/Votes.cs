

namespace GameForge.Models;


public class QuestionVote
{
    public string UserID { get; set; }=null!;
    public int QuestionID { get; set; }
    public bool IsUpvote { get; set; } = false;

    public required User User { get; set; }
    public required Question Question { get; set; }
}

public class AnswerVote
{
    public string UserID { get; set; }=null!;
    public int QuestionID { get; set; }
    public bool IsUpvote { get; set; }
    public required User User { get; set; }
    public required Question Question { get; set; }
}