// See https://aka.ms/new-console-template for more information
// À compter de .NET 6, le modèle d’application console C# génère des instructions de niveau supérieur

public class Program
{
    public static void Main()
    {
        // Initialisation des transactions
        var transactions = GetSampleTransactions();

        // Traitement des transactions
        TransactionProcessor processor = new TransactionProcessor();
        var processedTransactions = processor.ProcessTransactions(transactions);

        // Affichage des résultats
        DisplayProcessedTransactions(processedTransactions);

        Console.ReadLine();
    }

    private static List<Transaction> GetSampleTransactions()
    {
        return new List<Transaction>
        {
            new() {
                TransactionId = "TXN001",
                Amount = 100.50M,
                Currency = "USD",
                Timestamp = DateTime.Now.AddMinutes(-30),
                AccountFrom = "FR1234567890",
                AccountTo = "FR0987654321"
            },
            new() {
                TransactionId = "TXN002",
                Amount = 200.00M,
                Currency = "EUR",
                Timestamp = DateTime.Now.AddMinutes(-20),
                AccountFrom = "FR1234567890",
                AccountTo = "FR1122334455"
            },
            new() {
                TransactionId = "TXN003",
                Amount = 15000,
                Currency = "JPY",
                Timestamp = DateTime.Now.AddMinutes(-10),
                AccountFrom = "FR9988776655",
                AccountTo = "FR1234567890"
            },
            new() {
                TransactionId = "TXN004",
                Amount = 75.25M,
                Currency = "GBP",
                Timestamp = DateTime.Now,
                AccountFrom = "FR5566778899",
                AccountTo = "FR9988776655"
            }
        };
    }

    private static void DisplayProcessedTransactions(IEnumerable<Transaction> transactions)
    {
        Console.WriteLine("Processed Transactions:");
        foreach (var tx in transactions)
        {
            Console.WriteLine($"ID: {tx.TransactionId}, Amount: {tx.Amount} {tx.Currency}, " +
                            $"From: {tx.AccountFrom}, To: {tx.AccountTo}, Timestamp: {tx.Timestamp}");
        }
    }
}
