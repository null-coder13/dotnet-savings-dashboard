using DashboardApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DashboardApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpendingController(
    ILogger<SpendingController> logger,
    IFinancesRepository financesRepository)
    : ControllerBase
{
    [HttpGet("AverageSpending")]
    public ActionResult<APIResponse<decimal>> AverageSpending(int months)
    {
        logger.LogInformation("Getting average spending for last {Months} months", months);
        var result = financesRepository.AverageSpending(months);

        APIResponse<decimal> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }

    [HttpGet("TotalSpendingAndIncomeForYear")]
    public ActionResult<APIResponse<IDictionary<string, decimal>>> TotalSpendingAndIncomeForYear(int year)
    {
        logger.LogInformation("Getting total amounts for each category for year {Year}", year);
        var result = financesRepository.TotalIncomeAndSpendingForYear(year);

        APIResponse<IDictionary<string, decimal>> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return response;
    }

    [HttpGet("TotalSpendingForMonth")]
    public ActionResult<APIResponse<decimal>> TotalSpendingForMonth(int year, int month)
    {
        logger.LogInformation("Getting total spending for month: {Month} and year {Year}", month, year);
        var result = financesRepository.TotalSpendingForMonth(year, month);

        APIResponse<decimal> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }

    [HttpGet("TotalSpendingPerMonth")]
    public ActionResult<APIResponse<IDictionary<string, decimal>>> TotalSpendingPerMonth(int year)
    {
        logger.LogInformation("Getting total spending for year {Year}", year);
        var result = financesRepository.TotalSpendingPerMonth(year);

        APIResponse<IDictionary<string, decimal>> response = new()
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        return Ok(response);
    }
}