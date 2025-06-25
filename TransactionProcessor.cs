public class TransactionProcessor
{
    private static readonly Dictionary<string, decimal> _exchangeRates = new()
    {
        ["USD"] = 0.93M,
        ["GBP"] = 1.17M,
        ["JPY"] = 0.0062M
    };

    public List<Transaction> ProcessTransactions(List<Transaction> transactions)
    {
        var validTransactions = ValidateTransactions(transactions);
        return ConvertTransactionsToEuros(validTransactions);
    }

    private List<Transaction> ValidateTransactions(List<Transaction> transactions)
    {
        var validTransactions = new List<Transaction>(transactions.Count);
        var duplicateIds = new HashSet<string>();

        foreach (var tx in transactions)
        {
            if (duplicateIds.Contains(tx.TransactionId))
            {
                continue;
            }

            if (tx.Amount <= 0)
            {
                continue;
            }

            duplicateIds.Add(tx.TransactionId);
            validTransactions.Add(tx);
        }

        return validTransactions;
    }

    private List<Transaction> ConvertTransactionsToEuros(List<Transaction> validTransactions)
    {
        var processedTransactions = new List<Transaction>(validTransactions.Count);

        foreach (var tx in validTransactions)
        {
            processedTransactions.Add(tx.Currency == "EUR"
                ? tx
                : CreateEuroTransaction(tx));
        }

        return processedTransactions;
    }

    private Transaction CreateEuroTransaction(Transaction original)
    {
        return new Transaction
        {
            TransactionId = original.TransactionId,
            Amount = ConvertCurrency(original.Amount, original.Currency),
            Currency = "EUR",
            Timestamp = original.Timestamp,
            AccountFrom = original.AccountFrom,
            AccountTo = original.AccountTo
        };
    }

    private decimal ConvertCurrency(decimal amount, string fromCurrency)
    {
        // Retirer le Thread.Sleep pour le benchmark
        // (le garder seulement dans une version séparée pour les tests d'intégration)
        if (!_exchangeRates.TryGetValue(fromCurrency, out var rate))
        {
            throw new NotSupportedException($"Currency {fromCurrency} is not supported");
        }

        return amount * rate;
    }
}