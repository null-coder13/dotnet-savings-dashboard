using DashboardApi.Models;

namespace DashboardApi.Interfaces;

public interface IFinancesRepository
{
    // Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    // Task AddTransactionAsync(Transaction transaction);
    // Task UpdateTransactionByIdAsync(int id, Transaction transaction);
    // Task DeleteTransactionByIdAsync(int id);
    // Task<IEnumerable<Transaction>> GetTransactionsByMonthAsync(int month);
    // Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int month);
    // Task<IEnumerable<Transaction>> GetMonthlyTotalAmountsAsync();
    // Task<IEnumerable<decimal>> GetAllTimeTotalAmountAndTimeSpanAsync(); // TODO: Change return type to a new model
    void UploadTransactions(List<SavedTransaction> transactions);
    SavedTransaction? GetLastTransaction();



    // decimal GetTotalAmount();
    // decimal GetMonthlyTotalAmount(int month);
    // List<MonthTotal> GetMonthlyTotalAmounts();
    // HashSet<string> GetUniqueYears();
    // List<(string, decimal)> GetSumOfCategoriesForMonth(int month, int year);
    // decimal GetSumOfLastGivenMonths(int months);
    // List<Transaction> GetTransactionsForMonth(int month);

}
