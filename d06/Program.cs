using System;
using System.Collections.Generic;
using System.Linq;

namespace d06
{
    class Program
    {
        private static string test1 = @"0 2 7 0";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1.Split());
            P1(System.IO.File.ReadAllText(@"input.txt").Split());

            // P2(test1.Split());
            P2(System.IO.File.ReadAllText(@"input.txt").Split());

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var arr = input.Select(s => int.Parse(s)).ToArray();

            var set = new HashSet<string>();

            while (set.Add(string.Join(" ", arr))) {
                int maxi = 0, i = 0;
                for (i = 1; i < arr.Length; i++) {
                    if (arr[i] > arr[maxi]) {
                        maxi = i;
                    }
                }

                int blocks = arr[maxi];
                arr[maxi] = 0;
                i = maxi;
                while (blocks > 0) {
                    i = (i+1) % arr.Length;
                    arr[i]++;
                    blocks--;
                }
            }

            Console.WriteLine("Steps: {0}", set.Count());
        }

        static void P2(string[] input)
        {
            var arr = input.Select(s => int.Parse(s)).ToArray();

            var set = new Dictionary<string, int>();

            while (!set.ContainsKey(string.Join(" ", arr))) {
                set[string.Join(" ", arr)] = set.Count();

                int maxi = 0, i = 0;
                for (i = 1; i < arr.Length; i++) {
                    if (arr[i] > arr[maxi]) {
                        maxi = i;
                    }
                }

                int blocks = arr[maxi];
                arr[maxi] = 0;
                i = maxi;
                while (blocks > 0) {
                    i = (i+1) % arr.Length;
                    arr[i]++;
                    blocks--;
                }
            }

            Console.WriteLine("Steps: {0}", set.Count() - set[string.Join(" ", arr)]);
        }
    }
}
