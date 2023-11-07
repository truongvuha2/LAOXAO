using Microsoft.AspNetCore.Mvc;
using BusinessObject;

namespace LaoXao.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [HttpPost]
        public IActionResult Index(Account model)
        {
            if (ModelState.IsValid)
            {
                Account authenticatedAccount = IsValidCredentials(model.Username, model.Password);

                if (authenticatedAccount != null)
                {
                    if (authenticatedAccount.Role == 1)
                    {
                        // Redirect to the Manager action of the Songs controller for admin
                        return RedirectToAction("Manager", "Songs");
                    }
                    else if (authenticatedAccount.Role == 0)
                    {
                        // Redirect to the Index action of the Songs controller for regular users
                        return RedirectToAction("Index", "Songs");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }



        private Account IsValidCredentials(string username, string password)
        {
            using (var context = new MusicDbContext())
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
