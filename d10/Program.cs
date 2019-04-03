using System;
using System.Linq;

namespace d10
{
    class Program
    {
        private static string test1 = @"3,4,1,5";
        private static int[] suffix = { 17, 31, 73, 47, 23 };

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            P1(test1, 5);
            P1(System.IO.File.ReadAllText(@"input.txt"), 256);

            // P2("AoC 2017", 256);
            P2(System.IO.File.ReadAllText(@"input.txt"), 256);

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input, int size)
        {
            var arr = new int[size];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = i;
            }

            int skip = 0;
            int pos = 0;
            foreach (int len in input.Split(",").Select(n => int.Parse(n.Trim()))) {
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

            Console.WriteLine("Product: {0}", arr[0] * arr[1]);
        }

        static void P2(string input, int size)
        {
            var lengths = input.Select(c => (int) c).Concat(suffix);

            var arr = new int[size];
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

            string result = "";
            foreach (int n in dense) {
                result += string.Format("{0:x2}", n);
            }

            Console.WriteLine("Result: {0}", result);
        }

    }
}
