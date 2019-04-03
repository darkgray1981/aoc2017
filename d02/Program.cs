using System;

namespace d02
{
    class Program
    {
        private static string test1 = @"5 9 2 8
9 4 7 3
3 8 6 5";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(System.IO.File.ReadAllLines(@"input.txt"));
            // P2(test1.Split("\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            int sum = 0;
            foreach (string line in input) {
                int min = 99999999, max = -99999999;
                foreach (string chunk in line.Split(null as char[], StringSplitOptions.RemoveEmptyEntries)) {
                    int val = int.Parse(chunk);
                    if (val > max) {
                        max = val;
                    }
                    if (val < min) {
                        min = val;
                    }
                }

                sum += max - min;
            }

            Console.WriteLine("Sum: {0}", sum);
        }

        static void P2(string[] input)
        {
            int sum = 0;

            foreach (string line in input) {

                var vals = line.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);

                int r = 0;

                for (int i = 0; i < vals.Length-1; i++) {
                    int a = int.Parse(vals[i]);
                    for (int j = i+1; j < vals.Length; j++) {
                        int b = int.Parse(vals[j]);
                        if (a % b == 0) {
                            r = a/b;
                            goto done;
                        }
                        if (b % a == 0) {
                            r = b/a;
                            goto done;
                        }
                    }
                }

                done: sum += r;
            }

            Console.WriteLine("Sum: {0}", sum);
        }
    }
}
