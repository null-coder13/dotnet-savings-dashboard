using System.Net;
using DashboardApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DashboardApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TotalAmountController(ILogger<TotalAmountController> logger, IFinancesRepository financesRepository)
    : Controller
{
    /// <summary>
    /// Calculates the total amount in the account by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns>The total amount in the account.</returns>
    [HttpGet("TotalAllTimeAmount")]
    public ActionResult<APIResponse<decimal>> TotalAllTimeAmount()
    {
        logger.LogInformation("GetTotalAmount");

        // var totalAmount = fileRepository.GetTotalAmount();
        var totalAmount = financesRepository.TotalAllTimeAmount();

        APIResponse<decimal> response = new()
        {
            Result = totalAmount,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }

    /// <summary>
    /// Calculates the total amount for each month by subtracting the sum of all debits from the sum of all credits.
    /// </summary>
    /// <returns></returns>
    [HttpGet("TotalMonthlyAmountForYear")]
    public ActionResult<APIResponse<Dictionary<string, decimal>>> TotalMonthlyAmountForYear(int year)
    {
        logger.LogInformation("Getting monthly total amounts for year {year}", year);

        var monthlyTotalAmounts = financesRepository.MonthlyTotalAmountForYear(year);


        APIResponse<Dictionary<string, decimal>> response = new()
        {
            Result = monthlyTotalAmounts,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }
    
    [HttpGet("TotalAmountForMonth")]
    public ActionResult<APIResponse<decimal>> TotalAmountForMonth(int year, int month)
    {
        if (month < 1 || month > 12)
        {
            return BadRequest("Month must be between 1 and 12");
        }

        logger.LogInformation("TotalAmountForMonth: Getting total amount for month {Month}", month);

        var result = financesRepository.TotalAmountForMonth(year, month);

        APIResponse<decimal> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }
    
    
    /// <summary>
    /// Retrieves the sum of transactions for each category for a specific month.
    /// </summary>
    /// <param name="month">The month for which the sums are calculated.</param>
    /// <param name="year">The year for which the sums are calculated.</param>
    /// <returns>A list of tuples where the first item is the category and the second item is the sum of transactions for that category.</returns>
    [HttpGet("TotalAmountByCategoryForMonth")]
    public ActionResult<APIResponse<IDictionary<string, decimal>>> TotalAmountByCategoryForMonth(int year, int month)
    {
        logger.LogInformation("Getting total amounts for each category for year {Year} and month {Month}", year, month);
        var result = financesRepository.TotalAmountByCategoryByMonth(year, month);

        APIResponse<IDictionary<string, decimal>> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return response;
    }
}