using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90, baseline: true)]
[HardwareCounters(
    HardwareCounter.CacheMisses,
    HardwareCounter.BranchMispredictions,
    HardwareCounter.TotalCycles)]
public class TransactionProcessorBenchmark
{
    private List<Transaction> _smallBatch;
    private List<Transaction> _largeBatch;
    private List<Transaction> _veryLargeBatch;
    private TransactionProcessor _processor;

    [Params(4, 1000, 100000)]
    public int BatchSize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _processor = new TransactionProcessor();

        // Petit lot de transactions (4 éléments)
        _smallBatch = TransactionData.GetSampleTransactions();

        // Gros lot de transactions (1000 éléments)
        _largeBatch = TransactionData.GenerateLargeTransactionBatch(1000);

        _veryLargeBatch = TransactionData.GenerateLargeTransactionBatch(100000);

    }

    [Benchmark]
    public List<Transaction> ProcessBatch() =>
        _processor.ProcessTransactions(GetBatchForCurrentSize());

    private List<Transaction> GetBatchForCurrentSize()
    {
        return BatchSize switch
        {
            4 => _smallBatch,
            1000 => _largeBatch,
            100000 => _veryLargeBatch,
            _ => throw new NotImplementedException()
        };
    }

}