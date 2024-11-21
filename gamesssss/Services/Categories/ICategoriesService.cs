using Microsoft.AspNetCore.Mvc.Rendering;

namespace gamesssss.Services.Categories
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
