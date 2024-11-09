using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

[PrimaryKey("ThreadTopicID")]
public class ThreadTopic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ThreadTopicID { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int UserID { get; set; }
    public string Title { get; set; } = string.Empty;

    [HiddenInput(DisplayValue = false)]
    public DateTime CreationDate { get; set; }
    public string Message { get; set; } = string.Empty;
    public required List<string> Tag { get; set; }
    public int LatestReplyID { get; set; }
    public DateTime LatestReplyTime { get; set; }
    public int NumberOfReplies { get; set; }
    public ICollection<ThreadTopicReply> ThreadTopidcReplies { get; } = new List<ThreadTopicReply>();
    public User User { get; set; } = null!;
}

[PrimaryKey("ThreadTopicID", "UserID")]
public class ThreadTopicReply
{
    public int ThreadTopicID { get; set; }
    public string Message { get; set; } = string.Empty;
    public int UserID { get; set; }
    public DateTime CreationDate { get; set; }
    public required ThreadTopic ThreadTopic { get; set; }
    public required User User{ get; set; }
}

[NotMapped]
public class ThreadPost
{
    public required ThreadTopic ThreadTopic { get; set; }
    public List<ThreadTopicReply> ThreadTopicReplies { get; } = new List<ThreadTopicReply>();
}