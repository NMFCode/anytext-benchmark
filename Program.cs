using BenchmarkDotNet.Running;

namespace AnyText.PerformanceTests
{
    public class Program
    {

        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();

            //var nMetaUpdate = new AnyMetaInitializeUpdateBenchmark
            //{
            //    Model = "NMeta",
            //    TokenChanges = 100,
            //};
            //nMetaUpdate.LoadText();
            //for (int i = 0; i < 100; i++)
            //{
            //    nMetaUpdate.NextIteration();
            //    nMetaUpdate.Update();
            //}
        }

    }
}
