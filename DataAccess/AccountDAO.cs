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
        using var context = new MusicPrnContext();
        return context.Accounts.ToList();
    }

    // Lấy tài khoản dựa vào username
    public Account GetAccount(string username)
    {
        using var context = new MusicPrnContext();
        return context.Accounts.FirstOrDefault(a => a.Username == username);
    }

    // Kiểm tra xem một tài khoản có tồn tại dựa vào username và password
    public bool IsAccountExisted(string username, string password)
    {
        using var context = new MusicPrnContext();
        return context.Accounts.Any(a => a.Username == username && a.Password == password);
    }

    // Xóa một tài khoản dựa vào username
    public void RemoveAccount(string username)
    {
        using var context = new MusicPrnContext();
        var account = context.Accounts.FirstOrDefault(a => a.Username == username);
        if (account != null)
        {
            account.UserStatus = "Deleted";
            context.SaveChanges();
        }
    }

    // Kiểm tra xem username có tồn tại
    public bool IsUsernameExisted(string username)
    {
        using var context = new MusicPrnContext();
        return context.Accounts.Any(a => a.Username == username);
    }

    // Thêm một tài khoản
    public void AddAccount(Account account)
    {
        Account _account = GetAccount(account.Username);
        try
        {
            if (_account == null)
            {
                using var context = new MusicPrnContext();
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("The Username is already exist.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    // Cập nhật thông tin tài khoản
    public void UpdateAccount(Account account)
    {
        using var context = new MusicPrnContext();
        context.Entry(account).State = EntityState.Modified;
        context.SaveChanges();
    }

    // Cập nhật mật khẩu của tài khoản
    public void UpdatePassword(string username, string password)
    {
        using var context = new MusicPrnContext();
        var account = _context.Accounts.FirstOrDefault(a => a.Username == username);
        if (account != null)
        {
            account.Password = password;
            context.SaveChanges();
        }
    }
}