using System.Net;
using DashboardApi.Interfaces;
using DashboardApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashboardApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController(
    ILogger<TransactionController> logger,
    IFinancesRepository financesRepository)
    : Controller
{
    /// <summary>
    /// Retrieves a list of unique years from the transaction data.
    /// </summary>
    /// <returns>A list of unique years in the format "yyyy".</returns>
    [HttpGet("UniqueYears")]
    public ActionResult<APIResponse<ICollection<int>>> UniqueYears()
    {
        logger.LogInformation("Getting unique years");

        var uniqueYears = financesRepository.UniqueYears();

        APIResponse<ICollection<int>> response = new()
        {
            Result = uniqueYears,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }

    /// <summary>
    /// Gets all transactions for a specific month.
    /// </summary>
    /// <param name="year">The year for which the transaction are to be retrieved.</param>
    /// <param name="month">The month for which transactions are to be retrieved. Month is an integer where January is 1 and December is 12.</param>
    /// <returns>A list of transactions for the specified month.</returns>
    [HttpGet("TransactionsForMonth")]
    public ActionResult<APIResponse<ICollection<SavedTransaction>>> TransactionsForMonth(int year, int month)
    {
        logger.LogInformation("TransactionsForMonth: Getting transactions for year {year} month {month}", year, month);

        var result = financesRepository.TransactionsForMonth(year, month);

        APIResponse<List<SavedTransaction>> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }
}