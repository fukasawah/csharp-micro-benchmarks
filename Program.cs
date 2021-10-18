using BenchmarkDotNet.Running;

namespace CsharpBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            new ByteArrayEqualBenchmark().Test();
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }

    }
}