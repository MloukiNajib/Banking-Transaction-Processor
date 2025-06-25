// See https://aka.ms/new-console-template for more information
// À compter de .NET 6, le modèle d’application console C# génère des instructions de niveau supérieur

public class Transaction
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Timestamp { get; set; }
    public string AccountTo { get; set; }
    public string AccountFrom { get; set; }
}
