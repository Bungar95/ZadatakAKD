using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZadatakAKD.Models
{
    public class UsersModel: PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<IdentityUser> UsersList { get; set; }

        public void OnGet()
        {
            UsersList = _userManager.Users.ToList();
        }
    }
}
