using System.Collections.Concurrent;

public class TransactionProcessor
{
    /*** TransactionProcessor est responsable du traitement des transactions financières.
     * Il valide les transactions, supprime les doublons et convertit les montants en euros.
     * Les transactions sont validées pour s'assurer qu'elles ont un montant positif et un ID unique.
     * Les montants dans d'autres devises sont convertis en euros en utilisant des taux de change prédéfinis.
     */




/*** 
 * Utilisation de Dictionary pour les taux de change :
 *  - Recherche en O(1) au lieu du switch
 *  - Meilleure maintenabilité pour ajouter de nouvelles devises
*/
private static readonly Dictionary<string, decimal> _exchangeRates = new()
{
    ["USD"] = 0.93M,
    ["GBP"] = 1.17M,
    ["JPY"] = 0.0062M
};

    // Optimisation pour les gros lots
    public List<Transaction> ProcessTransactions(List<Transaction> transactions)
{
        // 1- Pré-filtrage parallèle des transactions invalides
        var validTransactions = transactions
            .AsParallel()
            .Where(tx => tx.Amount > 0)
            .GroupBy(tx => tx.TransactionId)
            .Select(g => g.First())
            .ToList();

        // 2- Conversion parallèle
        var result = new ConcurrentBag<Transaction>();
        Parallel.ForEach(validTransactions, tx =>
        {
            result.Add(tx.Currency == "EUR" ? tx : CreateEuroTransaction(tx));
        });

        return result.ToList();
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
    /*** Initialisation des listes avec capacité
     * new List<Transaction>(transactions.Count) réduit les réallocations
     * Visible dans la réduction de l'allocation mémoire
     */
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
    /*** 
     *  Retirer le Thread.Sleep pour le benchmark
     *   - Le temps de traitement est maintenant réaliste (ns/µs au lieu de ms)
     *   - Permet de mesurer les vraies performances de l'algorithme
     */
//  Thread.Sleep(10);  // simule un appel API lent
                       // (le garder seulement dans une version séparée pour les tests d'intégration)

    if (!_exchangeRates.TryGetValue(fromCurrency, out var rate))
    {
        throw new NotSupportedException($"Currency {fromCurrency} is not supported");
    }

    return amount * rate;
}
}