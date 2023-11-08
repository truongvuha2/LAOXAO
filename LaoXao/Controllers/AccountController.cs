using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LaoXao.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository accountRepository = null;
        public AccountController() => accountRepository = new AccountRepository();
        // GET: AccountController
        public ActionResult Index()
        {
            var accountList = accountRepository.GetAllAccounts();
            return View(accountList);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(string? username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var account = accountRepository.GetAccount(username);
            return View(account);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountRepository.AddAccount(account);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(account);
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(string? username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var account = accountRepository.GetAccount(username);
            return View(account);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string username, Account account)
        {
            try
            {
                if (username != account.Username)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    accountRepository.UpdateAccount(account);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(string? username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var account = accountRepository.GetAccount(username);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        public ActionResult DeleteConfirmed(string username, Account account)
        {
            try
            {
                accountRepository.RemoveAccount(username);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}