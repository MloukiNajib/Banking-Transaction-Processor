// See https://aka.ms/new-console-template for more information
// À compter de .NET 6, le modèle d’application console C# génère des instructions de niveau supérieur

using BenchmarkDotNet.Running;

public class Program
{
    public static void Main()
    {
        var summary = BenchmarkRunner.Run<TransactionProcessorBenchmark>();
                
    }
}
