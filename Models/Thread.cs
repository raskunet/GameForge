using System.ComponentModel.DataAnnotations;

namespace GameForge.Models;

public class Thread
{
    public int ID{ get; set; }
    public int AuthorID{ get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreationDate{ get; set; }
    public string Message { get; set; } = string.Empty;
    public required List<string> Tag { get; set; }
    public int LatestReplyID { get; set; }
    public DateTime LatestReplyTime{ get; set; }
    public int NumberOfReplies{ get; set; }
}


public class ThreadReply
{
    public int ParentThreadID { get; set; }
    public int ParentReplyID{ get; set; }
    public string Message { get; set; } = string.Empty;
    public int AuthorID { get; set; }
    public DateTime CreationDate { get; set; }
}