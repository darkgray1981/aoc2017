using System;
using System.Collections.Generic;

namespace d12
{
    class Program
    {
        private static string test1 = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            P2(test1.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var graph = new Dictionary<string, string[]>();

            foreach (string line in input) {
                string self = line.Split()[0];
                string[] friends = line.Split(" <-> ")[1].Split(", ");
                graph[self] = friends;
            }

            int count = 0;
            var stack = new Stack<string>();
            var set = new HashSet<string>();

            stack.Push("0");
            set.Add("0");

            while (stack.Count > 0) {
                var p = stack.Pop();
                count++;

                foreach (string f in graph[p]) {
                    if (!set.Contains(f)) {
                        stack.Push(f);
                        set.Add(f);
                    }
                }
            }

            Console.WriteLine("Programs: {0}", count);
        }

        static void P2(string[] input)
        {
            var graph = new Dictionary<string, string[]>();

            foreach (string line in input) {
                string self = line.Split()[0];
                string[] friends = line.Split(" <-> ")[1].Split(", ");
                graph[self] = friends;
            }

            int count = 0;
            var set = new HashSet<string>();

            foreach (string id in graph.Keys) {
                if (set.Contains(id)) {
                    continue;
                }

                count++;

                var stack = new Stack<string>();

                stack.Push(id);
                set.Add(id);

                while (stack.Count > 0) {
                    var p = stack.Pop();

                    foreach (string f in graph[p]) {
                        if (!set.Contains(f)) {
                            stack.Push(f);
                            set.Add(f);
                        }
                    }
                }
            }

            Console.WriteLine("Groups: {0}", count);
        }
    }
}
