using System;

namespace d11
{
    class Program
    {
        private static string test1 = @"ne,ne,s,s";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1);
            P1(System.IO.File.ReadAllText(@"input.txt"));

            // P2(test1.Split("\n"));
            P2(System.IO.File.ReadAllText(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input)
        {
            int q = 0, r = 0;

            foreach (string step in input.Split(",")) {

                switch (step) {
                    case "n":
                        r--;
                        break;
                    case "ne":
                        q++;
                        r--;
                        break;
                    case "se":
                        q++;
                        break;
                    case "s":
                        r++;
                        break;
                    case "sw":
                        q--;
                        r++;
                        break;
                    case "nw":
                        q--;
                        break;
                    default:
                        Console.WriteLine("Uh oh");
                        break;
                }
            }

            int distance = (Math.Abs(q - 0) + Math.Abs(q + r - 0 - 0) + Math.Abs(r - 0)) / 2;

            Console.WriteLine("Distance: {0}", distance);
        }

        static void P2(string input)
        {
            int q = 0, r = 0;
            int furthest = 0;

            foreach (string step in input.Split(",")) {

                switch (step) {
                    case "n":
                        r--;
                        break;
                    case "ne":
                        q++;
                        r--;
                        break;
                    case "se":
                        q++;
                        break;
                    case "s":
                        r++;
                        break;
                    case "sw":
                        q--;
                        r++;
                        break;
                    case "nw":
                        q--;
                        break;
                    default:
                        Console.WriteLine("Uh oh");
                        break;
                }

                int distance = (Math.Abs(q - 0) + Math.Abs(q + r - 0 - 0) + Math.Abs(r - 0)) / 2;
                furthest = Math.Max(distance, furthest);
            }

            Console.WriteLine("Max distance: {0}", furthest);
        }
    }
}
