using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public class TransactionService
{
    private List<Transaction> _transactions;
    private List<JournalEntry> _journalEntries;
    private List<Account> _accounts;

    public TransactionService()
    {
        _accounts = AAASampleData.GetAccounts();
        _transactions = AAASampleData.GetTransactions();
        _journalEntries = AAASampleData.GetJournalEntries();

        foreach (var transaction in _transactions)
        {
            transaction.JournalEntries = _journalEntries.Where(j => j.TransactionId == transaction.Id).ToList();
            foreach (var entry in transaction.JournalEntries)
            {
                entry.Transaction = transaction;
                entry.Account = _accounts.FirstOrDefault(a => a.Id == entry.AccountId);
            }
        }
    }

    public List<Transaction> GetAll() => _transactions;
    public Transaction? GetById(int id) => _transactions.FirstOrDefault(t => t.Id == id);
    public List<JournalEntry> GetJournalEntries(int transactionId) => _journalEntries.Where(j => j.TransactionId == transactionId).ToList();
    public List<Account> GetAccounts() => _accounts;

    public void Add(Transaction transaction)
    {
        transaction.Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;
        _transactions.Add(transaction);

        foreach (var entry in transaction.JournalEntries)
        {
            entry.Id = _journalEntries.Any() ? _journalEntries.Max(j => j.Id) + 1 : 1;
            entry.TransactionId = transaction.Id;
            entry.Transaction = transaction;
            entry.Account = _accounts.FirstOrDefault(a => a.Id == entry.AccountId);
            _journalEntries.Add(entry);
        }
    }

    public void Update(int id, Transaction transaction)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Reference = transaction.Reference;
        existing.Date = transaction.Date;
        existing.Type = transaction.Type;
        existing.Description = transaction.Description;
        existing.TotalAmount = transaction.TotalAmount;
        existing.Status = transaction.Status;
    }

    public void Delete(int id)
    {
        var transaction = GetById(id);
        if (transaction != null)
        {
            _journalEntries.RemoveAll(j => j.TransactionId == id);
            _transactions.Remove(transaction);
        }
    }
}
