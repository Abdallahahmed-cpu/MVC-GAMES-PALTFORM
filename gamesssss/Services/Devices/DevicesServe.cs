using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace gamesssss.Services.Devices
{
    public class DevicesServe : IDevicesServe
    {
        private readonly ApplicationDbContext _context;

        public DevicesServe(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
           return _context.Devices.Select
                 (d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                 .OrderBy(d => d.Text)
                 .AsNoTracking()
                 .ToList();
        }
    }
}
