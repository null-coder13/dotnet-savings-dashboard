using DataAccess;


var fr = new FileRepository();

DateTime today = DateTime.Now;

var printer = new Printer(fr);

int option = printer.PrintOptions();
Console.WriteLine();

while (option != 4)
{
    switch (option)
    {
        case 1:
            printer.PrintTransactionsForMonth(today.Month);
            break;
        case 2:
            printer.PrintTransactionByCategory(today.Month);
            break;
        case 3:
            printer.PrintMonthlyTotalAmounts();
            break;
        case 4:
            option = printer.PrintOptions();
            break;
        case 5:
            System.Console.WriteLine("Exiting the application. Goodbye!");
            break;
        default:
            System.Console.WriteLine("Invalid choice. Please enter a valid choice.");
            break;
    }
    if (option == 5) break;

    Console.WriteLine();
    option = printer.PrintOptions();
    Console.WriteLine();
}


