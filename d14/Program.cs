using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace d14
{
    class Program
    {
        private static string myInput = @"ugkiagan";
        private static string test1 = "flqrgnkx";

        private static Dictionary<char, string> hex2bin = new Dictionary<char, string>() {
            {'0', "0000"},
            {'1', "0001"},
            {'2', "0010"},
            {'3', "0011"},
            {'4', "0100"},
            {'5', "0101"},
            {'6', "0110"},
            {'7', "0111"},
            {'8', "1000"},
            {'9', "1001"},
            {'a', "1010"},
            {'b', "1011"},
            {'c', "1100"},
            {'d', "1101"},
            {'e', "1110"},
            {'f', "1111"},
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            P1(myInput);
            // P1(System.IO.File.ReadAllLines(@"input.txt"));

            P2(myInput);
            // P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input)
        {
            int used = 0;

            var hex2bits = new Dictionary<char, int>();
            foreach (char c in hex2bin.Keys) {
                hex2bits[c] = hex2bin[c].Count(ch => ch == '1');
            }

            for (int line = 0; line < 128; line++) {
                var kh = KnotHash(input + "-" + line.ToString());

                foreach (char c in kh) {
                    used += hex2bits[c];
                }
            }

            Console.WriteLine("Used: {0}", used);
        }

        static void P2(string input)
        {
            var grid = new char[128,128];

            for (int line = 0; line < 128; line++) {
                var kh = KnotHash(input + "-" + line.ToString());

                int pos = 0;

                foreach (char c in kh) {
                    foreach (char bit in hex2bin[c]) {
                        grid[line, pos++] = bit;
                    }
                }
            }

            int regions = 0;

            for (int y = 0; y < grid.GetLength(0); y++) {
                for (int x = 0; x < grid.GetLength(1); x++) {
                    if (grid[y, x] == '1') {
                        regions++;
                        wipe(grid, x, y);
                    }
                }
            }

            Console.WriteLine("Regions: {0}", regions);
        }

        static void wipe(char[,] grid, int x, int y) {
            if (y < 0 ||
                x < 0 ||
                y >= grid.GetLength(0) ||
                x >= grid.GetLength(1) ||
                grid[y,x] == '0')
            {
                return;
            }

            grid[y,x] = '0';

            wipe(grid, x, y-1);
            wipe(grid, x-1, y);
            wipe(grid, x+1, y);
            wipe(grid, x, y+1);
        }

        static string KnotHash(string input)
        {
            var lengths = input.Select(c => (int) c).Concat(new int[]{ 17, 31, 73, 47, 23 });

            var arr = new int[256];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = i;
            }

            int skip = 0;
            int pos = 0;

            for (int c = 0; c < 64; c++) {
                foreach (int len in lengths) {
                    if (len > 1) {
                        var arrCopy = new int[len];
                        for (int i = 0; i < len; i++) {
                            arrCopy[i] = arr[(pos+i) % arr.Length];
                        }

                        for (int i = 0; i < len; i++) {
                            arr[(pos+i) % arr.Length] = arrCopy[len-1-i];
                        }
                    }

                    pos = (pos+len+skip) % arr.Length;
                    skip++;
                }
            }

            var dense = new int[16];
            for (int i = 0; i < 16; i++) {
                for (int j = 0; j < 16; j++) {
                    dense[i] ^= arr[16*i+j];
                }
            }

            var result = new StringBuilder();
            foreach (int n in dense) {
                result.AppendFormat("{0:x2}", n);
            }

            return result.ToString();
        }
    }
}
