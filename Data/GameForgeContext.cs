using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameForge.Models;

namespace GameForge.Data
{
    public class GameForgeContext : DbContext
    {
        public GameForgeContext (DbContextOptions<GameForgeContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ThreadTopicReply>()
                .HasOne(e => e.ThreadTopic)
                .WithMany(e => e.ThreadTopidcReplies)
                .HasForeignKey(e => new { e.ThreadTopicID, e.UserID })
                .IsRequired();

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
                .HasOne(e => e.Question)
                .WithMany(e => e.Answers)
                .HasForeignKey(e => new { e.UserID, e.QuestionID })
                .IsRequired();

        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<ThreadTopic> ThreadTopic { get; set; }
        public DbSet<ThreadTopicReply> ThreadTopicReplies{ get; set; }
        public DbSet<GameForge.Models.Question> Question { get; set; } = default!;
        public DbSet<GameForge.Models.Answer> Answer { get; set; } = default!;
    }
}
