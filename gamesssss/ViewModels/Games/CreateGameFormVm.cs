using gamesssss.Attributes;
using Microsoft.AspNetCore.Http;

namespace gamesssss.ViewModels.Games
{
    public class CreateGameFormVm : GameVM
    {
        [AllowedExtensions(FileSettings.allowedExtention)]
        //[MaxFileSize(FileSettings.MaxSizeInMB * 1024 * 1024)] // Uncomment if you want to enforce size
        public IFormFile Cover { get; set; }

        // Additional properties specific to game creation, if not in GameVM, like:
        // public string Name { get; set; }
        // public string Description { get; set; }
        // public int CategoryId { get; set; }
    }
}
