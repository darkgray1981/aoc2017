using System;
using System.Linq;

namespace d13
{
    class Program
    {
        private static string test1 = @"0: 3
1: 2
4: 4
6: 4";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            int severity = 0;

            foreach (string line in input) {
                var parts = line.Split(": ", 2);
                int depth = int.Parse(parts[0]), range = int.Parse(parts[1]);

                if (depth % ((range-1) * 2) == 0) {
                    severity += depth * range;
                }
            }

            Console.WriteLine("Severity: {0}", severity);
        }

        static void P2(string[] input)
        {
            var depths = new int[input.Length];
            var ranges = new int[input.Length];

            for (int i = 0; i < input.Length; i++) {
                var parts = input[i].Split(": ", 2);
                depths[i] = int.Parse(parts[0]);
                ranges[i] = int.Parse(parts[1]);
            }

            int delay = 0;
            while (true) {

                bool caught = false;

                for (int i = 0; i < input.Length; i++) {
                    if ((depths[i]+delay) % ((ranges[i]-1) * 2) == 0) {
                        caught = true;
                        break;
                    }
                }

                if (!caught) {
                    break;
                }

                delay++;
            }

            Console.WriteLine("Delay: {0}", delay);
        }
    }
}
