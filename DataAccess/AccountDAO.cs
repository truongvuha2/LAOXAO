using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace DataAccess;

public class AccountDAO
{
    private AccountDAO() { }

    public static AccountDAO instance = null;
    private readonly MusicPrnContext _context = new MusicPrnContext();
    public static AccountDAO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AccountDAO();
            }
            return instance;
        }
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _context.Accounts.ToList();
    }

    // Lấy tài khoản dựa vào username
    public Account GetAccount(string username)
    {
        return _context.Accounts.FirstOrDefault(a => a.Username == username);
    }

    // Kiểm tra xem một tài khoản có tồn tại dựa vào username và password
    public bool IsAccountExisted(string username, string password)
    {
        return _context.Accounts.Any(a => a.Username == username && a.Password == password);
    }

    // Xóa một tài khoản dựa vào username
    public void RemoveAccount(string username)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Username == username);
        if (account != null)
        {
            account.UserStatus = "Deleted";
            _context.SaveChanges();
        }
    }

    // Kiểm tra xem username có tồn tại
    public bool IsUsernameExisted(string username)
    {
        return _context.Accounts.Any(a => a.Username == username);
    }

    // Thêm một tài khoản
    public void AddAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    // Cập nhật thông tin tài khoản
    public void UpdateAccount(Account account)
    {
        _context.Entry(account).State = EntityState.Modified;
        _context.SaveChanges();
    }

    // Cập nhật mật khẩu của tài khoản
    public void UpdatePassword(string username, string password)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Username == username);
        if (account != null)
        {
            account.Password = password;
            _context.SaveChanges();
        }
    }
}