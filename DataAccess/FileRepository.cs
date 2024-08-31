namespace DataAccess;

public class FileRepository : IFileRepository
{
    private readonly string _csvFilePath;
    private IEnumerable<Transaction> _transactions;

    // TODO: This class make be changed when implementing database
    public FileRepository()
    {
        _csvFilePath = "./transactions.csv";
        var reader = new FileReader(_csvFilePath);
        _transactions = reader.ReadFile().ToList();
    }

    public FileRepository(string filePath)
    {
        _csvFilePath = filePath;
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }
        var reader = new FileReader(_csvFilePath);
        _transactions = reader.ReadFile().ToList();
    }

    /// <summary>
    /// Calculates the total amount in the account by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns>The total amount in the account.</returns>
    public decimal GetTotalAmount()
    {
        decimal credits = _transactions.Where(t => t.Type == "Credit")
            .Sum(t => t.Amount);

        decimal debits = _transactions.Where(t => t.Type == "Debit")
            .Sum(t => t.Amount);

        return credits - debits;
    }

    /// <summary>
    /// Calculates the total amount for a specific month by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <param name="month">The month for which the total amount is calculated.</param>
    /// <returns>The total amount in the account for the specified month.</returns>
    public decimal GetMonthlyTotalAmount(int month)
    {
        decimal credits = _transactions.Where(t => t.Indicator == "Credit" && t.BookingDate.Month == month)
            .Sum(t => t.Amount);

        decimal debits = _transactions.Where(t => t.Indicator == "Debit" && t.BookingDate.Month == month)
            .Sum(t => t.Amount);

        return credits - debits;
    }

    /// <summary>
    /// Calculates the total amount for each month by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns>A dictionary where the key is the month and year in the format "MM-yyyy" and the value is the total amount for that month.</returns>
    public List<MonthTotal> GetMonthlyTotalAmounts()
    {

        return _transactions
                .GroupBy(t => new DateTime(t.BookingDate.Year, t.BookingDate.Month, 1))
                .Select(g => new MonthTotal
                {
                    Date = g.Key,
                    Total = g.Sum(t => t.Indicator == "Credit" ? t.Amount : -t.Amount)
                })
                .ToList();

        // Dictionary<string, decimal> result = new();
        //
        // foreach (var transaction in _transactions)
        // {
        //     string key = transaction.BookingDate.ToString("MM-yyyy");
        //     if (result.ContainsKey(key))
        //     {
        //         result[key] += transaction.Indicator == "Credit" ? transaction.Amount : -transaction.Amount;
        //     }
        //     else
        //     {
        //         result.Add(key, transaction.Indicator == "Credit" ? transaction.Amount : -transaction.Amount);
        //     }
        // }
        //
        // return result;
    }

    /// <summary>
    /// Retrieves a list of unique years from the transaction data.
    /// </summary>
    /// <returns>A list of unique years in the format "yyyy".</returns>
    public HashSet<string> GetUniqueYears()
    {
        HashSet<string> result = new();

        foreach (var transaction in _transactions)
        {
            string key = transaction.BookingDate.ToString("yyyy");
            if (!result.Contains(key))
            {
                result.Add(key);
            }
        }

        return result;
    }

    /// <summary>
    /// Retrieves the sum of debit transactions for each category for a specific month.
    /// </summary>
    /// <param name="month">The month for which the sums are calculated.</param>
    /// <returns>A list of tuples where the first item is the category and the second item is the sum of transactions for that category.</returns>
    public List<(string, decimal)> GetSumOfCategoriesForMonth(int month, int year)
    {
        return _transactions
            .Where(t => t.BookingDate.Month == month && t.BookingDate.Year == year)
            .Where(t => t.Indicator == "Debit")
            .GroupBy(t => t.Category)
            .Select(g => (Category: g.Key, Sum: g.Sum(t => t.Amount))).ToList();
    }

    /// <summary>
    /// Calculates the net sum of transactions for the last given number of months.
    /// </summary>
    /// <param name="months">The number of months to look back from the current date.</param>
    /// <returns>The net sum of transactions. Credits are added and debits are subtracted.</returns>
    public decimal GetSumOfLastGivenMonths(int months)
    {
        var dateThreshold = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        if (months > 0)
            dateThreshold = dateThreshold.AddMonths(-months);

        decimal netSum = _transactions
            .Where(t => t.BookingDate > dateThreshold)
            .Sum(t => t.Indicator == "Credit" ? t.Amount : t.Indicator == "Debit" ? -t.Amount : 0);

        return netSum;
    }

    /// <summary>
    /// Gets all transactions for a specific month.
    /// </summary>
    /// <param name="month">The month for which transactions are to be retrieved. Month is an integer where January is 1 and December is 12.</param>
    /// <returns>A list of transactions for the specified month.</returns>
    public List<Transaction> GetTransactionsForMonth(int month)
    {
        return _transactions.Where(t => t.BookingDate.Month == month).ToList();
    }
}
