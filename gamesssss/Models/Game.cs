using System.ComponentModel.DataAnnotations.Schema;

namespace gamesssss.Models
{
    public class Game : BaseEntity
    {

        public string Description { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public double AverageRating { get; set; } // Add this property

        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

        public ICollection<Review> Reviews { get; set; } // Make sure Reviews is here

        // Computed property to calculate the average rating
        

        // A User's specific rating for the game (for displaying the user's current rating)
        public int UserRating { get; set; }

    }
}
  //public string Description { get; set; } 

  //          public string Cover { get; set; } = string.Empty;

  //          public int CategoryId { get; set; } // Foreign key property

  //          [ForeignKey("CategoryId")]
  //          public Category Category { get; set; } // Navigation property

  //          public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();
  //      public ICollection<Review> Reviews { get; set; }  // This is crucial