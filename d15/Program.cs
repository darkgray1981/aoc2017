using System;

namespace d15
{
    class Program
    {
        private static string test1 = @"Generator A starts with 65
Generator B starts with 8921";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            const long factorA = 16807;
            const long factorB = 48271;
            const long divisor = 2147483647;

            long valA = long.Parse(input[0].Split()[4]);
            long valB = long.Parse(input[1].Split()[4]);

            int matches = 0;
            for (int i = 0; i < 40000000; i++) {
                valA *= factorA;
                valB *= factorB;

                valA %= divisor;
                valB %= divisor;

                if ((valA & 0xffff) == (valB & 0xffff)) {
                    matches++;
                }
            }

            Console.WriteLine("Matches: {0}", matches);
        }

        static void P2(string[] input)
        {
            const long factorA = 16807;
            const long factorB = 48271;
            const long divisor = 2147483647;

            long valA = long.Parse(input[0].Split()[4]);
            long valB = long.Parse(input[1].Split()[4]);

            int matches = 0;
            for (int i = 0; i < 5000000; i++) {
                do {
                    // valA *= factorA;
                    // valA %= divisor;
                    valA = (valA * factorA) % divisor;
                } while (valA % 4 != 0);

                do {
                    // valB *= factorB;
                    // valB %= divisor;
                    valB = (valB * factorB) % divisor;
                } while (valB % 8 != 0);

                if ((valA & 0xffff) == (valB & 0xffff)) {
                    matches++;
                }
            }

            Console.WriteLine("Matches: {0}", matches);
        }
    }
}
