namespace DataAccess;

public interface IFileReader
{
    IEnumerable<Transaction> ReadFile();
}
