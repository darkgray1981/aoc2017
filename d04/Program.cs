using System;
using System.Collections.Generic;

namespace d04
{
    class Program
    {
        private static string test1 = @"aa bb cc dd ee
aa bb cc dd aa
aa bb cc dd aaa";

        private static string test2 = @"abcde fghij
abcde xyz ecdab
a ab abc abd abf abj
iiii oiii ooii oooi oooo
oiii ioii iioi iiio";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1.Split("\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));
            // P2(test2.Split("\n"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            int count = 0;

            foreach (string line in input) {
                var set = new HashSet<string>();
                bool valid = true;

                foreach (string word in line.Split()) {
                    if (!set.Add(word)) {
                        valid = false;
                        break;
                    }
                }

                if (valid) {
                    count++;
                }
            }

            Console.WriteLine("Valid: {0}", count);
        }

        static void P2(string[] input)
        {
            int count = 0;

            foreach (string line in input) {
                var set = new HashSet<string>();
                bool valid = true;

                foreach (string word in line.Split()) {
                    var arr = word.ToCharArray();
                    Array.Sort(arr);

                    if (!set.Add(new string(arr))) {
                        valid = false;
                        break;
                    }
                }

                if (valid) {
                    count++;
                }
            }

            Console.WriteLine("Valid: {0}", count);
        }

    }
}
