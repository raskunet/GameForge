using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameForge.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public required string Title { get; set; }

        [StringLength(100)]
        [Required]
        public required string Description { get; set; }

        public required string ImageUrl { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        [StringLength(30)]
        [Required]
        public required string Category { get; set; }

        [Display(Name = "Gameplay Link")]
        [Url]
        public string? GameplayLink { get; set; }

        public ICollection<Review>? Reviews { get; set; }
        public double AverageRating
        {
            get
            {
                if (Reviews != null && Reviews.Any())
                {
                    return Reviews.Average(r => r.Rating);
                }
                return 0.0; // Return 0 if no reviews
            }
        }

        [Required]  // Ensures that every game is linked to a developer
        public int DeveloperId { get; set; }

        // Navigation property to Developer
        public Developer? Developer { get; set; }

        public bool IsPaid { get; set; } = true; // Default is paid

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; } = 0;

        // Calculated Price After Discount
        public decimal PriceAfterDiscount => IsPaid ? Price * (1 - DiscountPercentage / 100) : 0;

        // Define a discount percentage (e.g., 0 if no discount)
         public ICollection<Library>? Libraries { get; set; }


    }
}
