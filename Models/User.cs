using System.ComponentModel.DataAnnotations;

namespace GameForge.Models;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<ThreadTopic> ThreadTopics { get; } = new List<ThreadTopic>();
    public ICollection<ThreadTopicReply> ThreadTopicReplies { get; } = new List<ThreadTopicReply>();
    public ICollection<Question> Questions { get; } = new List<Question>();
    public ICollection<Answer> Answers { get; } = new List<Answer>();
    public ICollection<QuestionVote> QuestionVotes { get; } = new List<QuestionVote>();
    public ICollection<AnswerVote> AnswerVotes{ get; } = new List<AnswerVote>();
    public ICollection<Game> DownloadedGames { get; } = new List<Game>();
}