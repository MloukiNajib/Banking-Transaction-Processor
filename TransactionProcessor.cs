// See https://aka.ms/new-console-template for more information
// À compter de .NET 6, le modèle d’application console C# génère des instructions de niveau supérieur

public class TransactionProcessor
{
    public List<Transaction> ProcessTransactions(List<Transaction> transactions)
    {
        var validTransactions = ValidateTransactions(transactions);
        return ConvertTransactionsToEuros(validTransactions);
    }

    private List<Transaction> ValidateTransactions(List<Transaction> transactions)
    {
        var validTransactions = new List<Transaction>();
        var duplicateIds = new HashSet<string>();

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

        return validTransactions;
    }

    private List<Transaction> ConvertTransactionsToEuros(List<Transaction> validTransactions)
    {
        var processedTransactions = new List<Transaction>();

        foreach (var tx in validTransactions)
        {
            if (tx.Currency != "EUR")
            {
                processedTransactions.Add(CreateEuroTransaction(tx));
            }
            else
            {
                processedTransactions.Add(tx);
            }
        }

        return processedTransactions;
    }

    private Transaction CreateEuroTransaction(Transaction original)
    {
        decimal convertedAmount = ConvertCurrency(original.Amount, original.Currency);

        return new Transaction
        {
            TransactionId = original.TransactionId,
            Amount = convertedAmount,
            Currency = "EUR",
            Timestamp = original.Timestamp,
            AccountFrom = original.AccountFrom,
            AccountTo = original.AccountTo
        };
    }

    private decimal ConvertCurrency(decimal amount, string fromCurrency)
    {
        // Simulate slow external API call
        Thread.Sleep(10);

        return fromCurrency switch
        {
            "USD" => amount * 0.93M,
            "GBP" => amount * 1.17M,
            "JPY" => amount * 0.0062M,
            _ => throw new NotSupportedException($"Currency {fromCurrency} is not supported")
        };
    }
}