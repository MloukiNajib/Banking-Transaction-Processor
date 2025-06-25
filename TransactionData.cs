public class TransactionData
{
    private static readonly Random _random = new();
    private static readonly string[] _currencies = { "USD", "EUR", "GBP", "JPY" };

    public static List<Transaction> GetSampleTransactions()
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
            // ... (garder les autres transactions existantes)
        };
    }

    public static List<Transaction> GenerateLargeTransactionBatch(int count)
    {
        var transactions = new List<Transaction>();
        for (int i = 0; i < count; i++)
        {
            transactions.Add(new Transaction
            {
                TransactionId = $"TXN_{i:0000}",
                Amount = (decimal)(_random.NextDouble() * 1000),
                Currency = _currencies[_random.Next(_currencies.Length)],
                Timestamp = DateTime.Now.AddMinutes(-_random.Next(1440)),
                AccountFrom = $"FR{_random.NextInt64(1000000000, 9999999999)}",
                AccountTo = $"FR{_random.NextInt64(1000000000, 9999999999)}"
            });
        }
        return transactions;
    }
}