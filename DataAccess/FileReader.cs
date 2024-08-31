using System.Globalization;
using CsvHelper;

namespace DataAccess;

public class FileReader : IFileReader
{
    private readonly string _csvFilePath;

    public FileReader()
    {
        _csvFilePath = "./transactions.csv";
    }

    public FileReader(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }
        _csvFilePath = filePath;
    }

    public IEnumerable<Transaction> ReadFile()
    {
        using var reader = new StreamReader(_csvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var record in csv.GetRecords<Transaction>())
        {
            yield return record;
        }
    }

}
