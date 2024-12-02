using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameForge.Models;

[NotMapped]
public class Forum
{
    public required PaginatedList<ThreadTopic> ThreadTopics { get; set; }
    public required PaginatedList<Question> Questions { get; set; }
}

