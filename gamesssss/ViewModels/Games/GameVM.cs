using Microsoft.AspNetCore.Mvc.Rendering;

namespace gamesssss.ViewModels.Games
{
    public class GameVM
    {
        [MaxLength(length: 250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categroies { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<int> SelectedDevices { get; set; } = default;
        [Display(Name = "Supported Devices")]

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        public string Description { get; set; } = string.Empty;

    }
}
