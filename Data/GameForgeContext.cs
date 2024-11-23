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
            .HasKey(e => new { e.UserID, e.ThreadTopicID });

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

            modelBuilder.Entity<Cart>()
                .HasKey(e => new { e.CartID, e.UserID });

            modelBuilder.Entity<Collectables>()
                .HasKey(e => new { e.CollectableID, e.UserID });
            
            modelBuilder.Entity<Library>()
                .HasOne(e => e.User);

            modelBuilder.Entity<Wishlist>()
                .HasKey(e => new { e.WishlistID, e.UserID });
            
            modelBuilder.Entity<Game>()
                .HasKey(e => new { e.GameID, e.UserID });

        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<ThreadTopic> ThreadTopic { get; set; }
        public DbSet<ThreadTopicReply> ThreadTopicReplies{ get; set; }
        public DbSet<Question> Question { get; set; } = default!;
        public DbSet<Answer> Answer { get; set; } = default!;

        public DbSet<AnswerVote> AnswerVotes { get; set; } = default!;
        public DbSet<QuestionVote> QuestionVotes{ get; set; } = default!;

        public DbSet<Cart> Cart{ get; set; } = default!;
        public DbSet<Collectables> Collectables{ get; set; } = default!;
        public DbSet<Library> Library{ get; set; } = default!;
        public DbSet<Wishlist> Wishlist{ get; set; } = default!;
        public DbSet<Game> Game{ get; set; } = default!;
    }
}
