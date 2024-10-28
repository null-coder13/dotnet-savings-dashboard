using DashboardApi.Interfaces;
using DashboardApi.Models;

namespace DashboardApi.DataAccess;

public class FinancesRepository(FinancesDbContext context, ILogger<FinancesRepository> logger)
    : IFinancesRepository
{
    public void UploadTransactions(List<SavedTransaction> transactions)
    {
        logger.LogInformation("Uploading {Count} transactions...", transactions.Count);
        context.SavedTransactions.AddRange(transactions);
        context.SaveChanges();
    }

    public SavedTransaction? GetLastTransaction()
    {
        var transaction = context.SavedTransactions.OrderByDescending(t => t.BookingDate).FirstOrDefault();

        return transaction;
    }
}
