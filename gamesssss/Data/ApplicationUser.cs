using Microsoft.AspNetCore.Identity;

namespace gamesssss.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
