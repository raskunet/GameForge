using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

[PrimaryKey("ThreadTopicID")]
public class ThreadTopic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ThreadTopicID { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string UserID { get; set; }=null!;
    public string Title { get; set; } = string.Empty;

    [HiddenInput(DisplayValue = false)]
    public DateTime CreationDate { get; set; }
    public DateTime LastEditTime{ get; set; }
    public string Message { get; set; } = string.Empty;
    public required List<string> Tag { get; set; }
    public int LatestReplyID { get; set; }
    public DateTime LatestReplyTime { get; set; }
    public int NumberOfReplies { get; set; }
    public ICollection<ThreadTopicReply> ThreadTopidcReplies { get; } = new List<ThreadTopicReply>();
    public User User { get; set; } = null!;
}

[NotMapped]
public class ThreadPost
{
    public required ThreadTopic ThreadTopic { get; set; }
    public required bool DiscussFlag{ get; set; }
    //public List<ThreadTopicReply> ThreadTopicReplies { get; } = new List<ThreadTopicReply>();
}

[NotMapped]
public class ThreadCreateViewModel
{
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public List<string> Tag { get; set; } = null!;
    public SelectList? SelectTags { get; set; }
    public bool CanCreate { get; set; } = true;
}

[NotMapped]
public class ThreadEditViewModel
{
    public required int ThreadTopicID { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public required List<string> Tag { get; set; }
    public SelectList? SelectTags { get; set; }
    public bool CanEdit { get; set; } = true;

}

[NotMapped]
public class ThreadSearchViewModel
{
    public List<ThreadTopic>? ThreadTopics { get; set; }
    public SelectList? Tags { get; set; }
    public string? ThreadTag { get; set; }
    public string? ThreadSearchString { get; set; }
}