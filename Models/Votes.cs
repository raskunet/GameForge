

namespace GameForge.Models;


public class QuestionVote
{
    public int UserID { get; set; }
    public int QuestionID { get; set; }
    public bool IsUpvote { get; set; } = false;

    public required User User { get; set; }
    public required Question Question { get; set; }
}

public class AnswerVote
{
    public int UserID { get; set; }
    public int QuestionID { get; set; }
    public bool IsUpvote { get; set; }
    public required User User { get; set; }
    public required Question Question { get; set; }
}