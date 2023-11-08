using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IAccountRepository
    {
        /// <summary>
        ///  Lấy tất cả thông tin account
        /// </summary>
        IEnumerable<Account> GetAllAccounts();

        /// <summary>
        ///  Lấy thông tin bằng username
        /// </summary>
        Account GetAccount(string username);

        /// <summary>
        ///  Kiểm tra username và password đúng hay không
        /// </summary>
        bool IsAccountExisted(string username, string password);

        /// <summary>
        ///  Xóa bỏ account bằng cách cho status = "Deleted"
        /// </summary>
        void RemoveAccount(string username);
        /// <summary>
        ///  Xóa bỏ account bằng cách cho status = "Deleted"
        /// </summary>
        bool IsUserNameExisted(string username);
        /// <summary>
        ///  Tạo 1 account
        /// </summary>
        void AddAccount(Account account);
        /// <summary>
        /// Thay đổi thông tin của account
        /// </summary>
        void UpdateAccount(Account account);
        /// <summary>
        ///  Thay đổi password
        /// </summary>
        void UpdatePassword(string username, string password);
    }
}
