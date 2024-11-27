using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameForge.Models;

[PrimaryKey("ThreadTopicReplyID")]
public class ThreadTopicReply
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ThreadTopicReplyID{get;set;}
    public int? ParentReplyID{ get; set; }
    public int ThreadTopicID { get; set; }
    public string Message { get; set; } = string.Empty;
    public int UserID { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastEditTime{ get; set; }
    public required ThreadTopic ThreadTopic { get; set; }
    public required User User { get; set; }
    /*
        If A Reply is to a Main thread topic, below will be null
        Else it will reference the Reply to which it is is replying to something like reddit
    */
    public ThreadTopicReply? ParentReply{ get; set; }
}

[NotMapped]
public class ThreadReplyCreateViewModel
{
    public string ThreadTopicReplyText { get; set; } = null!;
    public int? ParentReplyID { get; set; }
    public int ThreadTopicID { get; set; }
    public bool CanCreate { get; set; } = true;
}

[NotMapped]
public class ThreadReplyEditViewModel
{
    public int ThreadTopicReplyID { get; set; }
    public  int ThreadTopicID{ get; set; }
    //public  int UserID{ get; set; }
    //public  int? ParentReplyID{ get; set; }
    public required string ThreadTopicReplyText{ get; set; }
    public bool CanEdit { get; set; } = true;
}