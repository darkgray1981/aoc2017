using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace d21
{
    class Program
    {
        private static string test1 = @"../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#";

        private static string initial = @".#./..#/###";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1.Split("\r\n"), 2);
            P1alt(System.IO.File.ReadAllLines(@"input.txt"), 5);

            // P2(test2.Split("\r\n"));
            P1alt(System.IO.File.ReadAllLines(@"input.txt"),  18);

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }


        private static void Transpose(char[][] arr)
        {
            for (int y = 0; y < arr.Length; y++) {
                for (int x = y+1; x < arr[0].Length; x++) {
                    char temp = arr[y][x];
                    arr[y][x] = arr[x][y];
                    arr[x][y] = temp;
                }
            }
        }

        private static void Mirror(char[][] arr)
        {
            for (int y = 0; y < arr.Length; y++) {
                Array.Reverse(arr[y]);
            }
        }

        private static string[] Variations(string input)
        {
            string[] result = new string[8];

            var parts = input.Split('/');

            char[][] arr = new char[parts.Length][];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = parts[i].ToCharArray();
            }

            for (int i = 0; i < 4; i++) {
                result[2*i] = string.Join("/", arr.Select(a => new string(a)));
                Transpose(arr);
                result[2*i+1] = string.Join("/", arr.Select(a => new string(a)));
                Mirror(arr);
            }

            return result;            
        }

        static void P1(string[] input, int iterations)
        {
            var rules = new Dictionary<string, string[]>();

            foreach (var line in input) {
                var parts = line.Split(" => ");
                rules[parts[0]] = parts[1].Split('/');
            }

            var square = initial;

            for (int i = 0; i < iterations; i++) {

                int size = square.IndexOf('/');
                int block = 2;
                if (size % 2 != 0) {
                    block = 3;
                }
                int width = size / block;

                var expanded = new List<string[]>();

                for (int y = 0; y < width; y++) {
                    int yoff = y * (size*block+block);
                    for (int x = 0; x < width; x++) {
                        int xoff = x * block;

                        var list = new List<string>();

                        for (int n = 0; n < block; n++) {
                            list.Add(square.Substring(yoff + xoff + (n+n*size), block));
                        }

                        var smashed = string.Join("/", list);

                        foreach (var cand in Variations(smashed)) {
                            // Console.WriteLine("Cand: {0}", cand);
                            if (rules.ContainsKey(cand)) {
                                expanded.Add(rules[cand]);
                                break;
                            }
                        }
                    }
                }

                var sb = new StringBuilder();
                for (int y = 0; y < width; y++) {
                    for (int n = 0; n < expanded[0].Length; n++) {
                        for (int x = 0; x < width; x++) {
                            sb.Append(expanded[y*width+x][n]);
                        }
                        sb.Append("/");
                    }
                }
                sb.Length--;

                // Console.WriteLine("{0}", sb);

                square = sb.ToString();
            }

            Console.WriteLine("Pixels: {0}", square.Where(c => c == '#').Count());
        }

        static void P1alt(string[] input, int iterations)
        {
            var rules = new Dictionary<string, string[]>();

            foreach (var line in input) {
                var parts = line.Split(" => ");
                var exp = parts[1].Split('/');
                foreach (var v in Variations(parts[0])) {
                    rules[v] = exp;
                }
            }

            var square = initial;

            for (int i = 0; i < iterations; i++) {

                int size = square.IndexOf('/');
                int block = 2;
                if (size % 2 != 0) {
                    block = 3;
                }
                int width = size / block;

                var expanded = new List<string[]>();

                for (int y = 0; y < width; y++) {
                    int yoff = y * (size*block+block);
                    for (int x = 0; x < width; x++) {
                        int xoff = x * block;

                        var list = new List<string>();

                        for (int n = 0; n < block; n++) {
                            list.Add(square.Substring(yoff + xoff + (n+n*size), block));
                        }

                        var smashed = string.Join("/", list);

                        if (rules.ContainsKey(smashed)) {
                            expanded.Add(rules[smashed]);
                        } else {
                            throw new Exception("Oh god");
                        }
                    }
                }

                var sb = new StringBuilder();
                for (int y = 0; y < width; y++) {
                    for (int n = 0; n < expanded[0].Length; n++) {
                        for (int x = 0; x < width; x++) {
                            sb.Append(expanded[y*width+x][n]);
                        }
                        sb.Append("/");
                    }
                }
                sb.Length--;

                // Console.WriteLine("{0}", sb);

                square = sb.ToString();
            }

            Console.WriteLine("Pixels: {0}", square.Where(c => c == '#').Count());
        }

    }
}
