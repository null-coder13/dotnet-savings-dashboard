using System.Globalization;
using System.Text.RegularExpressions;
using DashboardApi.Interfaces;
using DashboardApi.Models;

namespace DashboardApi.DataAccess;

public class FinancesRepository(FinancesDbContext context, ILogger<FinancesRepository> logger)
    : IFinancesRepository
{
    public Dictionary<string, decimal> TotalAmountByCategoryByMonth(int year, int month)
    {
        return context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Year == year &&
                                  transaction.BookingDate.Month == month)
            .GroupBy(transaction => transaction.Category)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(transaction => transaction.Amount)
            );
    }

    public Dictionary<string, decimal> MonthlyTotalAmountForYear(int year)
    {
        return context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Year == year)
            .GroupBy(transaction => transaction.BookingDate.Month)
            .ToDictionary(
                group => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                group => group.Sum(transaction =>
                    transaction.Indicator == "Credit" ? transaction.Amount : -transaction.Amount)
            );
    }

    public decimal TotalAllTimeAmount()
    {
        var total = context.SavedTransactions
            .Sum(transaction =>
                transaction.Indicator == "Credit" ? (double)transaction.Amount : (double)-transaction.Amount
            );

        return (decimal)total;
    }

    public decimal TotalAmountForMonth(int year, int month)
    {
        var totalAmount = context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Month == month && transaction.BookingDate.Year == year)
            .Sum(transaction =>
                transaction.Indicator == "Credit" ? (double)transaction.Amount : (double)-transaction.Amount);

        return (decimal)totalAmount;
    }

    public Dictionary<string, decimal> TotalIncomeAndSpendingForYear(int year)
    {
        return context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Year == year)
            .GroupBy(transaction => transaction.Indicator)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(transaction => transaction.Amount)
            );
    }

    public decimal TotalSpendingForMonth(int year, int month)
    {
        return (decimal)context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Month == month &&
                                  transaction.BookingDate.Year == year &&
                                  transaction.Indicator == "Debit")
            .Sum(transaction => (double)-transaction.Amount);
    }

    public Dictionary<string, decimal> TotalSpendingPerMonth(int year)
    {
        return context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Year == year &&
                                  transaction.Indicator == "Debit")
            .GroupBy(transaction => transaction.BookingDate.Month)
            .ToDictionary(
                group => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                group => group.Sum(transaction => -transaction.Amount)
            );
    }

    public decimal AverageSpending(int months)
    {
        var minDate = DateTime.Now.AddMonths(-months);
        var totals = context.SavedTransactions
            .Where(transaction => transaction.BookingDate >= minDate &&
                                  transaction.Indicator == "Debit")
            .GroupBy(transaction => new { transaction.BookingDate.Year, transaction.BookingDate.Month })
            .Select(group => group.Sum(transaction => (double)-transaction.Amount))
            .ToList();

        return (decimal)totals.Average();
    }

    public List<SavedTransaction> TransactionsForMonth(int year, int month)
    {
        var transactions = context.SavedTransactions
            .Where(transaction => transaction.BookingDate.Month == month && transaction.BookingDate.Year == year);

        return transactions.ToList();
    }

    public void UploadTransactions(List<SavedTransaction> transactions)
    {
        context.SavedTransactions.AddRange(transactions);
        context.SaveChanges();
    }

    public List<int> UniqueYears()
    {
        return context.SavedTransactions
            .Select(transaction => transaction.BookingDate.Year)
            .Distinct()
            .ToList();
    }

    public SavedTransaction? LastTransaction()
    {
        var transaction = context.SavedTransactions.OrderByDescending(t => t.BookingDate).FirstOrDefault();

        return transaction;
    }
}