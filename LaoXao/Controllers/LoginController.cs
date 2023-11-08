using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using DataAccess.Repository;
using System.Web;


namespace LaoXao.Controllers
{
    public class LoginController : Controller
    {
        IAccountRepository accountRepository = new AccountRepository();

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string username, string password)
        {


            if (accountRepository.IsAccountExisted(username, password))
            {
                Account account = accountRepository.GetAccount(username);
                if (account.Role == "Admin")
                {
                    if (account.UserStatus.Equals("Active"))
                    {
                        return RedirectToAction("Index", "Account");
                    }
                    // Redirect to the Manager action of the Songs controller for admin
                }
                else if (account.Role == "User")
                {
                    if (account.UserStatus.Equals("Active"))
                    {
                        // Tạo một cookie với thời gian sống ban đầu là 7 ngày
                        var option = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(1)
                        };
                        Response.Cookies.Append("Username", username, option);

                        return RedirectToAction("Index", "Songs");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            return RedirectToAction("Index");
        }




    }
}
