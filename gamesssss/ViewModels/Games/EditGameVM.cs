using gamesssss.Attributes;
using Microsoft.AspNetCore.Http;

namespace gamesssss.ViewModels.Games
{
    public class EditGameVM : GameVM
    {
        public int Id { get; set; }

        // Holds the current cover image path for displaying the existing cover in the edit view.
        public string? CurruntCover { get; set; }

        // For uploading a new cover image, if needed.
        public IFormFile? Cover { get; set; }
    }
}
