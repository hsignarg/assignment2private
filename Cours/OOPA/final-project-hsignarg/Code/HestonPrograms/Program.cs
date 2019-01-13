using System;

namespace HestonPrograms
{
    class Program
    {
        static void Main(string[] args)
        {
            Computations.TestHestonPricing();
            Computations.TestMCPricing();
            Computations.TestConcordance();
            Computations.TestAsian();
            Computations.TestLookBack();
            Computations.GraphError();
        }
    }
}
