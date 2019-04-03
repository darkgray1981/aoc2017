using System;
using System.Collections.Generic;

namespace d05
{
    class Program
    {
        private static string test1 = @"0
3
0
1
-3";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1.Split("\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var list = new List<int>(input.Length);

            foreach (string line in input) {
                list.Add(int.Parse(line));
            }

            int steps = 0;
            int next = 0;
            while (next < list.Count) {
                int current = list[next];
                list[next]++;
                next += current;
                steps++;
            }

            Console.WriteLine("Steps: {0}", steps);
        }

        static void P2(string[] input)
        {
            var list = new List<int>(input.Length);

            foreach (string line in input) {
                list.Add(int.Parse(line));
            }

            int steps = 0;
            int next = 0;
            while (next < list.Count) {
                int current = list[next];
                if (current >= 3) {
                    list[next]--;
                } else {
                    list[next]++;
                }
                next += current;
                steps++;
            }

            Console.WriteLine("Steps: {0}", steps);
        }
    }
}
