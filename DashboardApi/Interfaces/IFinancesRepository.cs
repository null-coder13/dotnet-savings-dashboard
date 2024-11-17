using DashboardApi.Models;

namespace DashboardApi.Interfaces;

public interface IFinancesRepository
{
    // Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    // Task AddTransactionAsync(Transaction transaction);
    // Task UpdateTransactionByIdAsync(int id, Transaction transaction);
    // Task DeleteTransactionByIdAsync(int id);
    // List<string> TransactionCategories();
    decimal AverageSpending(int months);
    SavedTransaction? LastTransaction();
    Dictionary<string, decimal> MonthlyTotalAmountForYear(int year);
    Dictionary<string, decimal> TotalAmountByCategoryByMonth(int year, int month);
    decimal TotalAllTimeAmount();
    decimal TotalAmountForMonth(int year, int month);
    Dictionary<string, decimal> TotalIncomeAndSpendingForYear(int year);
    decimal TotalSpendingForMonth(int year, int month);
    Dictionary<string, decimal> TotalSpendingPerMonth(int year);
    List<SavedTransaction> TransactionsForMonth(int year, int month);
    void UploadTransactions(List<SavedTransaction> transactions);
    List<int> UniqueYears();



    // decimal GetTotalAmount();
    // decimal GetMonthlyTotalAmount(int month);
    // List<MonthTotal> GetMonthlyTotalAmounts();
    // HashSet<string> GetUniqueYears();
    // List<(string, decimal)> GetSumOfCategoriesForMonth(int month, int year);
    // decimal GetSumOfLastGivenMonths(int months);
    // List<Transaction> GetTransactionsForMonth(int month);

}
