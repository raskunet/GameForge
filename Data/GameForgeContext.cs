using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GameForge.Data
{
    public class GameForgeContext(DbContextOptions<GameForgeContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e=>e.Id);
            modelBuilder.Entity<ThreadTopic>()
                .HasOne(e => e.User)
                .WithMany(e => e.ThreadTopics)
                .HasForeignKey(e => e.UserID)
                .IsRequired();

            modelBuilder.Entity<Question>()
                .HasOne(e => e.User)
                .WithMany(e => e.Questions)
                .HasForeignKey(e => e.AuthorID)
                .IsRequired();

            modelBuilder.Entity<Answer>()
                .HasKey(e => new { e.QuestionID, e.UserID });

            modelBuilder.Entity<QuestionVote>()
                .HasKey(e => new { e.QuestionID, e.UserID });

            modelBuilder.Entity<AnswerVote>()
                .HasKey(e => new { e.QuestionID, e.UserID });
            
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<ThreadTopic> ThreadTopic { get; set; }
        public DbSet<ThreadTopicReply> ThreadTopicReplies { get; set; }
        public DbSet<Question> Question { get; set; } = default!;
        public DbSet<Answer> Answer { get; set; } = default!;
        public DbSet<AnswerVote> AnswerVotes { get; set; } = default!;
        public DbSet<QuestionVote> QuestionVotes { get; set; } = default!;
        public DbSet<ThreadTag> ThreadTags { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;
        public DbSet<Review> Review { get; set; } = default!;
        public DbSet<Purchase> Purchase { get; set; } = default!;
        public DbSet<Developer> Developers { get; set; } = default!;
        public DbSet<GameProblem> GameProblems { get; set; } = default!;
    }
}
