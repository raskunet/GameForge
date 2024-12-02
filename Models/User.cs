using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GameForge.Models;

public class User : IdentityUser
{


    // public int Id { get; set; }

    // [Required(ErrorMessage = "Username is required")]
    // [StringLength(50, ErrorMessage = "Username can't be longer than 50 characters")]
    // public string Username { get; set; } = string.Empty;

    [Required]
    public DateTime CreationDate { get; set; }

    public bool IsDeveloper { get; set; } = false;


    // [Required(ErrorMessage = "Password is required")]
    // public string PasswordHash { get; set; } = string.Empty;

    // [Required(ErrorMessage = "Email is required")]
    // [EmailAddress(ErrorMessage = "Invalid Email Address")]
    // public string Email { get; set; } = string.Empty;




    public ICollection<ThreadTopic> ThreadTopics { get; } = new List<ThreadTopic>();
    public ICollection<ThreadTopicReply> ThreadTopicReplies { get; } = new List<ThreadTopicReply>();
    public ICollection<Question> Questions { get; } = new List<Question>();
    public ICollection<Answer> Answers { get; } = new List<Answer>();
    public ICollection<QuestionVote> QuestionVotes { get; } = new List<QuestionVote>();
    public ICollection<AnswerVote> AnswerVotes { get; } = new List<AnswerVote>();
}