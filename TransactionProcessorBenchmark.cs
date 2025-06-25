using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
public class TransactionProcessorBenchmark
{
    private List<Transaction> _smallBatch;
    private List<Transaction> _largeBatch;
    private TransactionProcessor _processor;

    [GlobalSetup]
    public void Setup()
    {
        _processor = new TransactionProcessor();

        // Petit lot de transactions (4 éléments)
        _smallBatch = TransactionData.GetSampleTransactions();

        // Gros lot de transactions (1000 éléments)
        _largeBatch = TransactionData.GenerateLargeTransactionBatch(1000);
    }

    [Benchmark(Baseline = true, Description = "Small batch")]
    public List<Transaction> ProcessSmallBatch() => _processor.ProcessTransactions(_smallBatch);

    [Benchmark(Description = "Large batch")]
    public List<Transaction> ProcessLargeBatch() => _processor.ProcessTransactions(_largeBatch);
}