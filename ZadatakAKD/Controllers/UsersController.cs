using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;

namespace ZadatakAKD.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ZadatakAKDContext _context;

        public UsersController(ZadatakAKDContext context)
        {
            _context = context;
        }

        // GET: Users
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["UserNameSortParm"] = sortOrder == "UserName" ? "userName_desc" : "UserName";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";

            var zadatakAKDContext = from a in _context.Users select a;

            switch (sortOrder)
            {
                case "userName":
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.UserName);
                    break;
                case "userName_desc":
                    zadatakAKDContext = zadatakAKDContext.OrderByDescending(a => a.UserName);
                    break;
                case "email":
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.Email);
                    break;
                case "email_desc":
                    zadatakAKDContext = zadatakAKDContext.OrderByDescending(a => a.Email);
                    break;
                default:
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.Id);
                    break;
            }

            return View(await zadatakAKDContext.AsNoTracking().ToListAsync());
        }
    }
}
