using System;

namespace d03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(361527);

            P2(361527);
            // P2(test1.Split("\n"));
            // P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(int target)
        {
            int layer = 0;
            int n = 1;

            while (target > n*n) {
                n += 2;
                layer++;
            }

            int pos = n*n - 2*layer;

            while (target < pos) {
                pos -= 2*layer;
            }

            Console.WriteLine("Distance: {0}", layer + Math.Abs(target - (pos+layer)));
        }

        static void P2(int target)
        {
            int[,] grid = new int[200,200];
            int x = 100, y = 100;

            int side = 1;
            grid[y,x] = 1;
            x++;

            while (true) {
                side += 2;

                for (int i = 0; i < side-2; i++) {
                    Fill(x, y, grid);
                    if (grid[y,x] >= target) {
                        goto done;
                    }
                    y -= 1;
                }

                for (int i = 1; i < side; i++) {
                    Fill(x, y, grid);
                    if (grid[y,x] >= target) {
                        goto done;
                    }
                    x -= 1;
                }

                for (int i = 1; i < side; i++) {
                    Fill(x, y, grid);
                    if (grid[y,x] >= target) {
                        goto done;
                    }
                    y += 1;
                }

                for (int i = 0; i < side; i++) {
                    Fill(x, y, grid);
                    if (grid[y,x] >= target) {
                        goto done;
                    }
                    // Console.WriteLine(grid[y,x]);
                    x += 1;
                }

            }


            done: ;

            // Console.WriteLine("{0}, {1}", y, x);
            Console.WriteLine("Value: {0}", grid[y,x]);

            // for (int i = 0; i < grid.GetLength(0); i++) {
            //     for (int j = 0; j < grid.GetLength(1); j++) {
            //         Console.Write(grid[i,j] + "\t");
            //     }
            //     Console.WriteLine();
            // }
        }

        private static void Fill(int x, int y, int[,] grid) {
            grid[y, x] += grid[y-1, x-1] + grid[y-1, x] + grid[y-1, x+1];
            grid[y, x] += grid[y, x-1] + grid[y, x+1];
            grid[y, x] += grid[y+1, x-1] + grid[y+1, x] + grid[y+1, x+1];
        }

    }
}
