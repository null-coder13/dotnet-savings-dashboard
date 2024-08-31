namespace DataAccess;

public class Printer
{
    private readonly FileRepository _fileRepository;
    public Printer(FileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public void PrintTransactionsForMonth(int month)
    {
        PrintHeader("Transactions for the month");
        Console.WriteLine("{0,-15} {1,-16} {2, -100}", "Date", "Amount", "Description");

        // Print transactions for given month
        var transactions = _fileRepository.GetTransactionsForMonth(month);
        foreach (var transaction in transactions)
        {
            Console.Write("{0,-15}", transaction.BookingDate.ToString("MM/dd/yyyy"));

            Console.ForegroundColor = transaction.Indicator == "Debit" ? ConsoleColor.Red : ConsoleColor.Green;
            System.Console.Write("{0,-16:C}", transaction.Amount);
            Console.ResetColor();

            Console.Write("{0,-100}\n", transaction.Description);
        }
    }

    public void PrintTransactionByCategory(int month)
    {
        PrintHeader("Transactions by category for the month");
        Console.WriteLine("{0,-25} {1,-26}", "Transaction Type", "Amount");

        var transactionsByCategory = _fileRepository.GetSumOfCategoriesForMonth(month, 2024);
        foreach (var category in transactionsByCategory)
        {
            System.Console.Write("{0,-25}", category.Item1.Length > 0 ? category.Item1 : "Other");

            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("{0,-26:C}", category.Item2);
            Console.ResetColor();
        }
    }

    public void PrintMonthlyTotalAmounts()
    {
        // PrintHeader("Total amounts for each month");
        // Console.WriteLine("{0,-10} {1,-10}", "Month", "Amount");
        //
        // var monthlyTotalAmounts = _fileRepository.GetMonthlyTotalAmounts();
        //
        // foreach (var month in monthlyTotalAmounts)
        // {
        //     System.Console.Write("{0,-10}", month.Key);
        //     Console.ForegroundColor = month.Value < 0 ? ConsoleColor.Red : ConsoleColor.Green;
        //     System.Console.WriteLine("{0,-10:C}", month.Value);
        //     Console.ResetColor();
        // }
    }

    public int PrintOptions()
    {
        System.Console.Write("1. Print overall summary\n2. Print summary by category \n3. Print Amount for each month\n4. Print Options to choose\n5. Exit\nEnter your choice: ");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
        {
            System.Console.WriteLine("Invalid choice. Please enter a valid choice.\nEnter your choice: ");
        }
        return choice;
    }

    private void PrintHeader(string header)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine($"### {header} ###");
        Console.ResetColor();
    }
}
