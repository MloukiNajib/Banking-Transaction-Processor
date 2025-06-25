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

    public static List<Transaction> GenerateLargeTransactionBatch(int count)
    {
        var transactions = new List<Transaction>();
        var now = DateTime.Now; // Évite d'appeler DateTime.Now à chaque itération

        for (int i = 0; i < count; i++)
        {
            transactions.Add(new Transaction
            {
                TransactionId = $"TXN_{i:0000}",
                Amount = (decimal)(_random.NextDouble() * 1000),
                Currency = _currencies[_random.Next(_currencies.Length)],
                Timestamp = now.AddMinutes(-_random.Next(1440)),
                AccountFrom = $"FR{_random.NextInt64(1000000000, 9999999999)}",
                AccountTo = $"FR{_random.NextInt64(1000000000, 9999999999)}"
            });
        }
        return transactions;
    }
}