using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameForge.Models;

[NotMapped]
public class Forum
{
    public required List<ThreadTopic> ThreadTopicsFilter { get; set; }
    public required List<Question> FilterQuestionFilter { get; set; }
}

