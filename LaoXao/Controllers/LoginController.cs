using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;

namespace LaoXao.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Account model)
        {
            Account authenticatedAccount = IsValidCredentials(model.Username, model.Password);

            if (authenticatedAccount != null)
            {
                if (authenticatedAccount.Role == "Admin")
                {
                    // Redirect to the Manager action of the Songs controller for admin
                    return RedirectToAction("Manager", "Songs");
                }
                else if (authenticatedAccount.Role == "User")
                {
                    TempData["Username"] = authenticatedAccount.Username;
                    return RedirectToAction("Index", "Songs");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }




        private Account IsValidCredentials(string username, string password)
        {
            using (var context = new MusicPrnContext())
            {
                var account = context.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);

                if (account != null)
                {
                    return account;
                }

                return null;
            }
        }


    }
}
