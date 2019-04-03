using System;

namespace d09
{
    class Program
    {
        private static string test1 = @"{{{}}}"; // 6
        private static string test2 = @"{{<ab>},{<ab>},{<ab>},{<ab>}}"; // 9
        private static string test3 = @"{{<!!>},{<!!>},{<!!>},{<!!>}}"; // 9
        private static string test4 = @"{{{},{},{{}}}}"; // 16
        private static string test5 = @"{{<a!>},{<a!>},{<a!>},{<ab>}}"; // 3

        private static string test6 = @"<!!!>>"; // 0
        private static string test7 = @"<{o'i!a,<{i<a>"; // 10
        private static string test8 = @"<{!>}>"; // 2

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test5);
            P1(System.IO.File.ReadAllText(@"input.txt"));

            // P2(test6);
            // P2(test7);
            // P2(test8);
            P2(System.IO.File.ReadAllText(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input)
        {
            int score = 0;
            int depth = 0;
            bool inGarbage = false;

            for (int i = 0; i < input.Length; i++) {

                char c = input[i];

                if (c == '!') {
                    i++;
                } else if (c == '>') {
                    inGarbage = false;
                } else if (inGarbage) {
                    continue;
                } else if (c == '<') {
                    inGarbage = true;
                } else if (c == '}') {
                    depth--;
                } else if (c == '{') {
                    depth++;
                    score += depth;
                }
            }

            Console.WriteLine("Score: {0}", score);
        }

        static void P2(string input)
        {
            int removed = 0;
            int depth = 0;
            bool inGarbage = false;

            for (int i = 0; i < input.Length; i++) {

                char c = input[i];

                if (c == '!') {
                    i++;
                } else if (c == '>') {
                    inGarbage = false;
                } else if (inGarbage) {
                    removed++;
                    continue;
                } else if (c == '<') {
                    inGarbage = true;
                } else if (c == '}') {
                    depth--;
                } else if (c == '{') {
                    depth++;
                }
            }

            Console.WriteLine("Removed: {0}", removed);
        }
    }
}
