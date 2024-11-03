using DashboardApi.Interfaces;
using DashboardApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashboardApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController(
    ILogger<FileUploadController> logger,
    IFinancesRepository financesRepository,
    IFileReader fileReader)
    : ControllerBase
{
    [HttpPost("UploadTransactions")]
    public IActionResult UploadTransactions(IFormFile file)
    {
        var transactions = fileReader.ReadFormFile(file).ToList();

        var lastSavedTransaction = financesRepository.LastTransaction() ?? new SavedTransaction
        {
            BookingDate = DateTime.MinValue
        };

        var newTransactions = transactions
                .Where(t => t.BookingDate > lastSavedTransaction.BookingDate)
                .Select(t => new SavedTransaction
                {
                    Indicator = t.Indicator,
                    Type = t.Type,
                    Amount = t.Amount,
                    Description = t.Description,
                    BookingDate = t.BookingDate,
                    Category = t.Category
                })
                .ToList();

        logger.LogInformation("Saving {NewTransactionsCount} new transactions to the database.", newTransactions.Count);
        financesRepository.UploadTransactions(newTransactions);

        return Ok(newTransactions.Count);
    }
}

