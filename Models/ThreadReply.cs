using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;



[PrimaryKey("ThreadTopicID", "UserID")]
public class ThreadTopicReply
{
    public int ThreadTopicID { get; set; }
    public string Message { get; set; } = string.Empty;
    public int UserID { get; set; }
    public DateTime CreationDate { get; set; }
    public required ThreadTopic ThreadTopic { get; set; }
    public required User User { get; set; }
}

[NotMapped]
public class ThreadReplyCreateViewModel
{
    public string ThreadTopicReplyText { get; set; } = null!;
    public int ThreadTopicID{ get; set; }
}

[NotMapped]
public class ThreadReplyEditViewModel
{
    public required int ThreadTopicID{ get; set; }
    public required int UserID{ get; set; }
    public required string ThreadTopicReplyText{ get; set; }
}