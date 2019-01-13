using System;
using HestonModel;

namespace HestonPrograms
{
    public class Computations
    {
        /// <summary>
        /// Heston Pricing, Task 2.2
        /// </summary>
        public static void TestHestonPricing()
        {
            double r = 0.025;
            double theta = 0.0398;
            double kappa = 1.5768;
            double sigma = 0.5751;
            double rho = -0.5711;
            double v = 0.0175;
            double S = 100;
            double K = 100;
            double[] Ts = { 1, 2, 3, 4, 15 };

            Console.WriteLine("Testing Heston Pricing...");

            HestonPricing heston = new HestonPricing(kappa, theta, sigma, rho, v);
            double pricing = new double();
            for (int i = 0; i < Ts.Length; i++)
            {
                pricing = heston.HestonOption(r, Ts[i], S, K, PayoffType.Call);
                Console.WriteLine("Price for maturity T={0} is c={1}", Ts[i], pricing);
            }
        }

        /// <summary>
        /// Monte Carlo Pricing, Task 2.3
        /// </summary>
        public static void TestMCPricing()
        {
            double r = 0.1;
            double theta = 0.06;
            double kappa = 2;
            double sigma = 0.4;
            double rho = 0.5;
            double v = 0.04;
            double S = 100;
            double K = 100;
            double[] Ts = { 1, 2, 3, 4, 15 };
            int paths = 100000;
            int steps = 365;

            MonteCarloPricing mc = new MonteCarloPricing(kappa, theta, sigma, rho, v, steps);
            double pricing = new double();
            for (int j = 0; j < 10; j++)
            {
                Console.WriteLine("Testing Monte Carlo Pricing...({0})",j);
                for (int i = 0; i < Ts.Length; i++)
                {
                    pricing = mc.HestonEuropeanOptionMC(r, Ts[i], S, K, paths, PayoffType.Call);
                    Console.WriteLine("Price for maturity T={0} is c={1}", Ts[i], pricing);
                } 
            }

        }

        /// <summary>
        /// Results for Figure 2 of Task 2.4
        /// </summary>
        public static void GraphError()
        {
            double r = 0.1;
            double theta = 0.06;
            double kappa = 2;
            double sigma = 0.4;
            double rho = 0.5;
            double v = 0.04;
            double S = 100;
            double K = 100;
            double[] Ts = { 1, 2, 3, 4, 15 };
            int steps = 365;

            double priceHeston = new double();
            double[] pricing = new double[7];
            int paths = new int();
            double price = new double();

            HestonPricing heston = new HestonPricing(kappa, theta, sigma, rho, v);
            MonteCarloPricing mc = new MonteCarloPricing(kappa, theta, sigma, rho, v, steps);
            for (int i = 0; i < Ts.Length; i++)
            {
                paths = 1;
                priceHeston = heston.HestonOption(r, Ts[i], S, K, PayoffType.Call);
                for (int j = 1; j < 6; j++)
                {
                    paths *= 10;
                    pricing[j] = 0; 
                    for (int k=0; k<6;  k++)
                    price = mc.HestonEuropeanOptionMC(r, Ts[i], S, K, paths, PayoffType.Call);
                    pricing[j] += Math.Abs(priceHeston - price) / 5;
                }
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", Ts[i], pricing[1], pricing[2], pricing[3], pricing[4], pricing[5]);
            }
        }


        /// <summary>
        /// Results for Table and Figure 1 of Task 2.2
        /// </summary>
        public static void TestConcordance()
        {
            double r = 0.1;
            double theta = 0.06;
            double kappa = 2;
            double sigma = 0.4;
            double rho = 0.5;
            double v = 0.04;
            double S = 100;
            double K = 100;
            double[] Ts = { 1, 2, 3, 4, 15 };
            int steps = 365;

            double[] pricing = new double[7];
            int paths = new int();

            HestonPricing heston = new HestonPricing(kappa, theta, sigma, rho, v);
            MonteCarloPricing mc = new MonteCarloPricing(kappa, theta, sigma, rho, v, steps);
            for (int i = 0; i < Ts.Length; i++)
            {
                paths = 1;
                pricing[0] = heston.HestonOption(r, Ts[i], S, K, PayoffType.Call);
                for (int j = 1; j < 6; j++)
                {
                    paths *= 10;
                    pricing[j] = mc.HestonEuropeanOptionMC(r, Ts[i], S, K, paths, PayoffType.Call);
                }
                Console.WriteLine("{0} & {1} & {2} & {3} & {4} & {5} & {6} & {7} & {8} & {9} & {10} & {11} & {12} & {13}",
                Ts[i], Math.Round(pricing[0], 3), Math.Round(pricing[1], 3), Math.Round(Math.Abs(pricing[0] - pricing[1]), 3), Math.Round(pricing[2], 3),
                    Math.Round(Math.Abs(pricing[0] - pricing[2]), 3), Math.Round(pricing[3], 3), Math.Round(Math.Abs(pricing[0] - pricing[3]), 3),
                     Math.Round(pricing[4], 3), Math.Round(Math.Abs(pricing[0] - pricing[4]), 3), Math.Round(pricing[5], 3),
                    Math.Round(Math.Abs(pricing[0] - pricing[5]), 3));
            }
        }

        /// <summary>
        /// Monte Carlo pricing for Asian Option, task 2.7
        /// </summary>
        public static void TestAsian()
        {
            Console.WriteLine("Testing HestonAsianOption");
            double r = 0.1;
            double theta = 0.06;
            double kappa = 2;
            double sigma = 0.4;
            double rho = 0.5;
            double v = 0.04;
            double S = 100;
            double K = 100;
            double[] Ts = { 1, 2, 3};
            int paths = 100000;
            int steps = 365;

            double[][] dates = new double[3][];
            dates[0] = new double[2];
            dates[0][0] = 0.75;
            dates[0][1]= 1 ;
            dates[1] = new double[7];
            dates[1][0] = 0.25;
            dates[1][1] = 0.5;
            dates[1][2] = 0.75;
            dates[1][3] = 1;
            dates[1][4] = 1.25;
            dates[1][5] = 1.5;
            dates[1][6] = 1.75;
            dates[2] = new double[3];
            dates[2][0] = 0.25;
            dates[2][1] = 0.5;
            dates[2][2] = 0.75;

            MonteCarloPricing mc = new MonteCarloPricing(kappa, theta, sigma, rho, v, steps);
            double pricing = new double();
                for (int i = 0; i < Ts.Length; i++)
                {
                    pricing = mc.HestonAsianOptionMC(r, Ts[i], S, K, paths, dates[i], PayoffType.Call);
                    Console.WriteLine("Price for maturity T={0} is c={1}", Ts[i], pricing);
                }
        }

        /// <summary>
        /// Monte Carlo pricing for lookback option, task 2.8
        /// </summary>
        public static void TestLookBack()
        {
            Console.WriteLine("Testing HestonLookbackOption");
            double r = 0.1;
            double theta = 0.06;
            double kappa = 2;
            double sigma = 0.4;
            double rho = 0.5;
            double v = 0.04;
            double S = 100;
            double[] Ts = { 1, 3, 5, 7, 9 };
            int paths = 100000;
            int steps = 365;

            MonteCarloPricing mc = new MonteCarloPricing(kappa, theta, sigma, rho, v, steps);
            double pricing = new double();

            for (int i = 0; i < Ts.Length; i++)
            {
                pricing = mc.HestonLookbackOption(r, Ts[i], S, paths);
                Console.WriteLine("Price for maturity T={0} is c={1}", Ts[i], pricing);
            }
        }
    }
}
