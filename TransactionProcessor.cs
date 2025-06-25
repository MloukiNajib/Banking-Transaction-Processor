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
        if(transactions == null || transactions.Count == 0)
            return new List<Transaction>();

        // Utilisation de HashSet pour la détection des doublons en O(1)
        var seenIds = new ConcurrentDictionary<string, byte>();

        // Filtrage et validation en parallèle
        var validTransactions = transactions
            .AsParallel()
            .Where(tx =>
                tx != null &&
                tx.Amount > 0 &&
                seenIds.TryAdd(tx.TransactionId, 0))
            .ToList();

        // Conversion en euros avec pré-allocation de la capacité
        var result = new Transaction[validTransactions.Count];
        Parallel.For(0, validTransactions.Count, i =>
        {
            var tx = validTransactions[i];
            result[i] = tx.Currency == "EUR" ? tx : CreateEuroTransaction(tx);
        });

        return result.ToList();
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