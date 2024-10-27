using System.Globalization;
using CsvHelper;
using DashboardApi.Interfaces;
using DashboardApi.Models;

namespace DashboardApi.DataAccess;

public class FileReader : IFileReader
{
    public FileReader()
    {
    }

    public IEnumerable<Transaction> ReadFormFile(IFormFile file)
    {
        if (file is null)
        {
            throw new ArgumentNullException(nameof(file), "File cannot be null.");
        }

        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var record in csv.GetRecords<Transaction>())
        {
            yield return record;
        }
    }

    public IEnumerable<Transaction> ReadLocalFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found.", path);
        }
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var record in csv.GetRecords<Transaction>())
        {
            yield return record;
        }
    }
}
