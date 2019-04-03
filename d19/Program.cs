using System;
using System.Text;

namespace d19
{
    class Program
    {
        private static string test1 = @"     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ 
";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            P2(test1.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            int[] pos = { 0, 0};
            pos[1] = input[0].IndexOf('|');

            char dir = 'D';

            var sb = new StringBuilder();

            bool done = false;

            while (!done) {

                switch (dir) {
                    case 'U':
                        pos[0]--;
                        break;
                    case 'R':
                        pos[1]++;
                        break;
                    case 'D':
                        pos[0]++;
                        break;
                    case 'L':
                        pos[1]--;
                        break;
                }

                switch (input[pos[0]][pos[1]]) {
                    case '-':
                    case '|':
                        break;
                    case '+':
                        if (dir == 'U' || dir == 'D') {
                            dir = (input[pos[0]][pos[1]-1] == ' ' ? 'R' : 'L');
                        } else {
                            dir = (input[pos[0]-1][pos[1]] == ' ' ? 'D' : 'U');
                        }
                        break;
                    case ' ':
                        done = true;
                        break;
                    default:
                        sb.Append(input[pos[0]][pos[1]]);
                        break;
                }
            }

            Console.WriteLine("Letters: {0}", sb);
        }

        static void P2(string[] input)
        {
            int[] pos = { 0, 0};
            pos[1] = input[0].IndexOf('|');

            char dir = 'D';

            int steps = 0;

            bool done = false;

            while (!done) {

                steps++;

                switch (dir) {
                    case 'U':
                        pos[0]--;
                        break;
                    case 'R':
                        pos[1]++;
                        break;
                    case 'D':
                        pos[0]++;
                        break;
                    case 'L':
                        pos[1]--;
                        break;
                }

                switch (input[pos[0]][pos[1]]) {
                    case '-':
                    case '|':
                        break;
                    case '+':
                        if (dir == 'U' || dir == 'D') {
                            dir = (input[pos[0]][pos[1]-1] == ' ' ? 'R' : 'L');
                        } else {
                            dir = (input[pos[0]-1][pos[1]] == ' ' ? 'D' : 'U');
                        }
                        break;
                    case ' ':
                        done = true;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Steps: {0}", steps);
        }
    }
}
