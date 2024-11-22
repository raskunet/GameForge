using System;
using System.ComponentModel.DataAnnotations;

namespace GameForge.Models
{
    public class Review
    {
        public int Id { get; set; }


        [Required]
        public int UserId { get; set; } 

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Review text cannot exceed 500 characters.")]
        public string? Comment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        // Foreign Key to Game
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
