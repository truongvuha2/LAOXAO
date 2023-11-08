using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void AddAccount(Account account)
        => AccountDAO.Instance.AddAccount(account);

        public Account GetAccount(string username)
        => AccountDAO.Instance.GetAccount(username);

        public IEnumerable<Account> GetAllAccounts()
        => AccountDAO.Instance.GetAllAccounts();

        public bool IsAccountExisted(string username, string password)
        => AccountDAO.Instance.IsAccountExisted(username, password);

        public bool IsUserNameExisted(string username)
        => AccountDAO.Instance.IsUsernameExisted(username);

        public void RemoveAccount(string username)
        => AccountDAO.Instance.RemoveAccount(username);

        public void UpdateAccount(Account account)
        => AccountDAO.Instance.UpdateAccount(account);

        public void UpdatePassword(string username, string password)
        => AccountDAO.Instance.UpdatePassword(username, password);
    }
}
