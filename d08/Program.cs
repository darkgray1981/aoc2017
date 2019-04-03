using System;
using System.Collections.Generic;
using System.Linq;

namespace d08
{
    class Program
    {

        private static string test1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

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
            var reg = new Dictionary<string, int>();

            foreach (string line in input) {
                var parts = line.Split();

                string rx = parts[0];
                string ry = parts[4];
                string perform = parts[1];
                int byVal = int.Parse(parts[2]);
                string cmp = parts[5];
                int toVal = int.Parse(parts[6]);

                if (!reg.ContainsKey(rx)) {
                    reg[rx] = 0;
                }
                if (!reg.ContainsKey(ry)) {
                    reg[ry] = 0;
                }

                bool result = false;

                switch (cmp) {
                    case "==":
                        result = (reg[ry] == toVal);
                        break;
                    case "!=":
                        result = (reg[ry] != toVal);
                        break;
                    case "<":
                        result = (reg[ry] < toVal);
                        break;
                    case ">":
                        result = (reg[ry] > toVal);
                        break;
                    case "<=":
                        result = (reg[ry] <= toVal);
                        break;
                    case ">=":
                        result = (reg[ry] >= toVal);
                        break;

                    default:
                        throw new Exception("Oh no");
                }

                if (result) {
                    if (perform == "inc") {
                        reg[rx] += byVal;
                    } else {
                        reg[rx] -= byVal;
                    }
                }

            }

            Console.WriteLine("Largest: {0}", reg.Values.Max());
        }

        static void P2(string[] input)
        {
            var reg = new Dictionary<string, int>();
            int biggest = 0;

            foreach (string line in input) {
                var parts = line.Split();

                string rx = parts[0];
                string ry = parts[4];
                string perform = parts[1];
                int byVal = int.Parse(parts[2]);
                string cmp = parts[5];
                int toVal = int.Parse(parts[6]);

                if (!reg.ContainsKey(rx)) {
                    reg[rx] = 0;
                }
                if (!reg.ContainsKey(ry)) {
                    reg[ry] = 0;
                }

                bool result = false;

                switch (cmp) {
                    case "==":
                        result = (reg[ry] == toVal);
                        break;
                    case "!=":
                        result = (reg[ry] != toVal);
                        break;
                    case "<":
                        result = (reg[ry] < toVal);
                        break;
                    case ">":
                        result = (reg[ry] > toVal);
                        break;
                    case "<=":
                        result = (reg[ry] <= toVal);
                        break;
                    case ">=":
                        result = (reg[ry] >= toVal);
                        break;

                    default:
                        throw new Exception("Oh no");
                }

                if (result) {
                    if (perform == "inc") {
                        reg[rx] += byVal;
                    } else {
                        reg[rx] -= byVal;
                    }

                    if (reg[rx] > biggest) {
                        biggest = reg[rx];
                    }
                }

            }

            Console.WriteLine("Largest: {0}", biggest);
        }
    }
}
