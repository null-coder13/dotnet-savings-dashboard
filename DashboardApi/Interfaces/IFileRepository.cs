using DashboardApi.Models;

namespace DashboardApi.Interfaces;

public interface IFileRepository
{
    decimal GetTotalAmount();
    decimal GetMonthlyTotalAmount(int month);
    List<MonthTotal> GetMonthlyTotalAmounts();
    HashSet<string> GetUniqueYears();
    List<(string, decimal)> GetSumOfCategoriesForMonth(int month, int year);
    decimal GetSumOfLastGivenMonths(int months);
    List<Transaction> GetTransactionsForMonth(int month);
}
