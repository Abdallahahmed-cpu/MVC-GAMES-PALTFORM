
namespace gamesssss.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; } 
        public string? Comment { get; set; }
        public int Rating { get; set; }


        // Foreign key to Game
        public int GameId { get; set; }
        public Game Game { get; set; }

        // Foreign key to ApplicationUser (User who wrote the review)
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime DateCreated { get; internal set; }
    }
}
