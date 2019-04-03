using System;
using System.Collections.Generic;

namespace d22
{
    class Program
    {
        private static string test1 = @"..#
#..
...";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1.Split("\r\n"), 10000);
            P1(System.IO.File.ReadAllLines(@"input.txt"), 10000);

            // P2(test1.Split("\r\n"), 10000000);
            P2(System.IO.File.ReadAllLines(@"input.txt"), 10000000);

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        enum Dir { Up, Right, Down, Left };
        enum State { Clean, Weakened, Infected, Flagged };

        static void P1(string[] input, int target)
        {
            var grid = new Dictionary<(int,int), bool>();

            for (int y = 0; y < input.Length; y++) {
                var line = input[y];
                for (int x = 0; x < line.Length; x++) {
                    grid[(x,y)] = (line[x] == '#');
                }
            }

            var pos = (input[0].Length/2, input.Length/2);

            var turns = new Dir[] { Dir.Up, Dir.Right, Dir.Down, Dir.Left };
            var dir = Dir.Up;
            int infections = 0;
            int bursts = 0;

            for (; bursts < target; bursts++) {
                if (!grid.ContainsKey(pos)) {
                    grid[pos] = false;
                }

                if (grid[pos]) {
                    dir = turns[((int)dir + 1) % 4];
                } else {
                    dir = turns[(4 + (int)dir - 1) % 4];
                    infections++;
                }

                // Console.WriteLine("Turned {0} with {1} infections at {2} bursts", Enum.GetName(typeof(Dir), dir), infections, bursts);

                grid[pos] = !grid[pos];

                switch (dir) {
                    case Dir.Up:
                        pos = (pos.Item1, pos.Item2-1);
                        break;
                    case Dir.Right:
                        pos = (pos.Item1+1, pos.Item2);
                        break;
                    case Dir.Down:
                        pos = (pos.Item1, pos.Item2+1);
                        break;
                    case Dir.Left:
                        pos = (pos.Item1-1, pos.Item2);
                        break;
                }
            }

            Console.WriteLine("Infections: {0}", infections);
        }

        static void P2(string[] input, int target)
        {
            var grid = new Dictionary<(int,int), State>();

            for (int y = 0; y < input.Length; y++) {
                var line = input[y];
                for (int x = 0; x < line.Length; x++) {
                    // grid[(x,y)] = (line[x] == '#' ? State.Infected : State.Clean);
                    if (line[x] == '#') {
                        grid[(x,y)] = State.Infected;
                    }
                }
            }

            var pos = (input[0].Length/2, input.Length/2);

            // var states = new State[] { State.Clean, State.Weakened, State.Infected, State.Flagged };
            // var turns = new Dir[] { Dir.Up, Dir.Right, Dir.Down, Dir.Left };
            var dir = Dir.Up;
            int infections = 0;
            int bursts = 0;

            for (; bursts < target; bursts++) {
                State current;

                if (grid.ContainsKey(pos)) {
                    current = grid[pos];
                } else {
                    current = State.Clean;
                }

                switch (current) {
                    case State.Clean:
                        // dir = turns[(4 + (int)dir - 1) % 4]; // turn left
                        dir = (Dir)((4 + (int)dir - 1) % 4);
                        break;
                    case State.Weakened:
                        infections++;
                        break;
                    case State.Infected:
                        // dir = turns[((int)dir + 1) % 4]; // turn right
                        dir = (Dir)(((int)dir + 1) % 4);
                        break;
                    case State.Flagged:
                        // dir = turns[((int)dir + 2) % 4]; // turn around
                        dir = (Dir)(((int)dir + 2) % 4);
                        break;
                }

                // grid[pos] = states[((int)grid[pos] + 1) % 4];
                grid[pos] = (State)(((int)current + 1) % 4);

                // Console.WriteLine("Turned {0} with {1} infections at {2} bursts", Enum.GetName(typeof(Dir), dir), infections, bursts);

                switch (dir) {
                    case Dir.Up:
                        // pos = (pos.Item1, pos.Item2-1);
                        pos.Item2--;
                        break;
                    case Dir.Right:
                        // pos = (pos.Item1+1, pos.Item2);
                        pos.Item1++;
                        break;
                    case Dir.Down:
                        // pos = (pos.Item1, pos.Item2+1);
                        pos.Item2++;
                        break;
                    case Dir.Left:
                        // pos = (pos.Item1-1, pos.Item2);
                        pos.Item1--;
                        break;
                }
            }

            Console.WriteLine("Infections: {0}", infections);
        }
    }
}
