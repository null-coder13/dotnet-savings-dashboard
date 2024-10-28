
using DashboardApi.Models;

namespace DashboardApi.Interfaces;

public interface IFileReader
{
    IEnumerable<Transaction> ReadLocalFile(string path);
    IEnumerable<Transaction> ReadFormFile(IFormFile file);
}
