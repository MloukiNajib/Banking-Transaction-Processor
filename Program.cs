// See https://aka.ms/new-console-template for more information
// À compter de .NET 6, le modèle d’application console C# génère des instructions de niveau supérieur



// #pragma warning disable IDE0090
var transactions = new List<Transaction>
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
// #pragma warning restore IDE0090

TransactionProcessor processor = new TransactionProcessor();
var processedTransactions = processor.ProcessTransaction(transactions);

Console.WriteLine("Processed Transactions:");
foreach (var tx in processedTransactions)
{
    Console.WriteLine($"ID: {tx.TransactionId}, Amount: {tx.Amount} {tx.Currency}, From: {tx.AccountFrom}, To: {tx.AccountTo}, Timestamp: {tx.Timestamp}");
}

Console.ReadLine();

public class Transaction
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Timestamp { get; set; }
    public string AccountTo { get; set; }
    public string AccountFrom { get; set; }
}

public class TransactionProcessor
{
    public List<Transaction> ProcessTransaction(List<Transaction> transactions)
    {
        var validTransactions = new List<Transaction>();
        var duplicateIds = new HashSet<string>();

        // Check the duplacates and validate
        foreach (var tx in transactions)
        {
            if (duplicateIds.Contains(tx.TransactionId))
            {
                Console.WriteLine($"Duplicate transaction ID:{tx.TransactionId}");
                continue;
            }

            if (tx.Amount <= 0)
            {
                Console.WriteLine($"Invalid amount for transaction ID:{tx.TransactionId}");
                continue;
            }

            duplicateIds.Add(tx.TransactionId);
            validTransactions.Add(tx);
        }

        // Apply currency conversion naive approche
        var processedTransactions = new List<Transaction>();
        foreach (var tx in validTransactions)
        {
            if (tx.Currency != "EUR")
            {
                decimal convertedAmount = ConvertCurrency(tx.Amount, tx.Currency);
                processedTransactions.Add(new Transaction
                {
                    TransactionId = tx.TransactionId,
                    Amount = convertedAmount,
                    Currency = "EUR",
                    Timestamp = tx.Timestamp,
                    AccountFrom = tx.AccountFrom,
                    AccountTo = tx.AccountTo
                });
            }
            else
            {
                processedTransactions.Add(tx);
            }
        }

        return processedTransactions;
    }

    private decimal ConvertCurrency(decimal amount, string fromCurrency)
    {
        // Simulate slow external API call
        System.Threading.Thread.Sleep(10);

        return fromCurrency switch
        {
            "USD" => amount * 0.93M,
            "GBP" => amount * 1.17M,
            "JPY" => amount * 0.0062M,
            _ => throw new NotSupportedException($"Currency {fromCurrency}")
        };
    }
}