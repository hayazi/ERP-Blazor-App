using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public class AccountService
{
    private List<Account> _accounts;

    public AccountService()
    {
        _accounts = AAASampleData.GetAccounts();
    }

    public List<Account> GetAll() => _accounts;
    public Account? GetById(int id) => _accounts.FirstOrDefault(a => a.Id == id);

    public void Add(Account account)
    {
        account.Id = _accounts.Any() ? _accounts.Max(a => a.Id) + 1 : 1;
        if (account.ParentAccountId.HasValue)
        {
            account.ParentAccount = GetById(account.ParentAccountId.Value);
        }
        _accounts.Add(account);
    }

    public void Update(int id, Account account)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Code = account.Code;
        existing.Name = account.Name;
        existing.Type = account.Type;
        existing.ParentAccountId = account.ParentAccountId;
        existing.ParentAccount = account.ParentAccountId.HasValue ? GetById(account.ParentAccountId.Value) : null;
        existing.IsActive = account.IsActive;
    }

    public void Delete(int id)
    {
        var account = GetById(id);
        if (account != null)
        {
            _accounts.Remove(account);
        }
    }
}
