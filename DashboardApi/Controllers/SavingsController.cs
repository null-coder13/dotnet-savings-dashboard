using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using System.Net;

namespace DashboardApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SavingsController : ControllerBase
{
    private readonly ILogger<SavingsController> _logger;
    private readonly IFileRepository _fileRepository;

    public SavingsController(ILogger<SavingsController> logger, IFileRepository fileRepository)
    {
        _logger = logger;
        _fileRepository = fileRepository;
    }

    /// <summary>
    /// Calculates the total amount in the account by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns>The total amount in the account.</returns>
    [HttpGet("GetTotalAmount")]
    public ActionResult<APIResponse<int>> GetTotalAmount()
    {
        _logger.LogInformation("GetTotalAmount");

        var totalAmount = _fileRepository.GetTotalAmount();

        APIResponse<decimal> response = new();
        response.Result = totalAmount;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Calculates the total amount for a specific month by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <param name="month">The month for which the total amount is calculated.</param>
    /// <returns>The total amount in the account for the specified month.</returns>
    [HttpGet("GetMonthlyTotalAmount")]
    public ActionResult<APIResponse<decimal>> GetMonthlyTotalAmount(int month)
    {
        _logger.LogInformation("GetMonthlyTotalAmount: Getting total amount for month {month}", month);

        var totalAmount = _fileRepository.GetMonthlyTotalAmount(month);

        APIResponse<decimal> response = new();
        response.Result = totalAmount;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Calculates the total amount for each month by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetMonthlyTotalAmounts")]
    public ActionResult<APIResponse<List<MonthTotal>>> GetMonthlyTotalAmounts()
    {
        _logger.LogInformation("GetMonthlyTotalAmounts");

        var monthlyTotalAmounts = _fileRepository.GetMonthlyTotalAmounts();

        APIResponse<List<MonthTotal>> response = new();
        response.Result = monthlyTotalAmounts;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a list of unique years from the transaction data.
    /// </summary>
    /// <returns>A list of unique years in the format "yyyy".</returns>
    [HttpGet("GetUniqueYears")]
    public ActionResult<APIResponse<ICollection<string>>> GetUniqueYears()
    {
        _logger.LogInformation("GetUniqueYears");

        var uniqueYears = _fileRepository.GetUniqueYears();

        APIResponse<ICollection<string>> response = new();
        response.Result = uniqueYears;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the sum of transactions for each category for a specific month.
    /// </summary>
    /// <param name="month">The month for which the sums are calculated.</param>
    /// <param name="year">The year for which the sums are calculated.</param>
    /// <returns>A list of tuples where the first item is the category and the second item is the sum of transactions for that category.</returns>
    [HttpGet("GetSumOfCategoriesForMonth")]
    public ActionResult<APIResponse<ICollection<CategoryTotal>>> GetSumOfCategoriesForMonth(int month, int year)
    {
        _logger.LogInformation("GetSumOfCategoriesForMonth: Getting sum of categories for month: {month} year: {year}", month, year);

        List<(string, decimal)> result = _fileRepository.GetSumOfCategoriesForMonth(month, year);

        List<CategoryTotal> categoryTotals = new();
        foreach (var (category, amount) in result)
        {
            categoryTotals.Add(new CategoryTotal { Category = category, Amount = amount });
        }

        APIResponse<List<CategoryTotal>> response = new();
        response.Result = categoryTotals;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Calculates the net sum of transactions for the last given number of months.
    /// </summary>
    /// <param name="months">The number of months to look back from the current date.</param>
    /// <returns>The net sum of transactions. Credits are added and debits are subtracted.</returns>
    [HttpGet("GetSumOfLastGivenMonths")]
    public ActionResult<APIResponse<decimal>> GetSumOfLastGivenMonths(int months)
    {
        _logger.LogInformation("GetSumOfLastGivenMonths: Getting sum of last {months} months", months);

        var result = _fileRepository.GetSumOfLastGivenMonths(months);

        APIResponse<decimal> response = new();
        response.Result = result;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

    /// <summary>
    /// Gets all transactions for a specific month.
    /// </summary>
    /// <param name="month">The month for which transactions are to be retrieved. Month is an integer where January is 1 and December is 12.</param>
    /// <returns>A list of transactions for the specified month.</returns>
    [HttpGet("TransactionsForMonth")]
    public ActionResult<APIResponse<ICollection<Transaction>>> TransactionsForMonth(int month)
    {
        _logger.LogInformation("TransactionsForMonth: Getting transactions for month {month}", month);

        var result = _fileRepository.GetTransactionsForMonth(month);

        APIResponse<List<Transaction>> response = new();
        response.Result = result;
        response.StatusCode = HttpStatusCode.OK;

        return Ok(response);
    }

}
