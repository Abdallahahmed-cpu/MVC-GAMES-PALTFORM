using Microsoft.AspNetCore.Mvc.Rendering;

namespace gamesssss.Services.Devices
{
    public interface IDevicesServe
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
