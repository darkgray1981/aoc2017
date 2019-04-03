using System;

namespace d01
{
    class Program
    {
        private static string test1 = "12131415";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(System.IO.File.ReadAllText(@"input.txt"));
            // P1(test1);
            P2(System.IO.File.ReadAllText(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input)
        {
            int sum = 0;
            string s = input;
            for (int i = 0; i < s.Length; i++) {
                if (s[i] == s[(i+1) % s.Length]) {
                    sum += s[i] - '0';
                }
            }

            Console.WriteLine("Sum: {0}", sum);
        }

        static void P2(string input)
        {
            int sum = 0;
            string s = input;
            for (int i = 0; i < s.Length; i++) {
                if (s[i] == s[(i+s.Length/2) % s.Length]) {
                    sum += s[i] - '0';
                }
            }

            Console.WriteLine("Sum: {0}", sum);
        }
    }
}
