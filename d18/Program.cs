using System;
using System.Collections.Generic;
using System.Linq;

namespace d18
{
    class Program
    {
        private static string test1 = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
        private static string test2 = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            P2(test2.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var reg = new long[1 + 'z' - 'a'];
            long frequency = 0;
            long recovered = 0;

            for (int i = 0; i >= 0 && i < input.Length; i++) {
                var parts = input[i].Split();

                long rx = parts[1][0];
                long vx = -1;

                if (rx >= 'a' && rx <= 'z') {
                    rx = rx - 'a';
                    vx = reg[rx];
                } else {
                    vx = long.Parse(parts[1]);
                }

                long ry = -1;
                long vy = -1;

                if (parts.Length == 3) {
                    ry = parts[2][0];

                    if (ry >= 'a' && ry <= 'z') {
                        ry = ry - 'a';
                        vy = reg[ry];
                    } else {
                        vy = int.Parse(parts[2]);
                    }
                }


                switch (parts[0]) {
                    case "snd":
                        frequency = vx;
                        break;
                    case "set":
                        reg[rx] = vy;
                        break;
                    case "add":
                        reg[rx] += vy;
                        break;
                    case "mul":
                        reg[rx] *= vy;
                        break;
                    case "mod":
                        reg[rx] %= vy;
                        break;
                    case "rcv":
                        if (vx != 0) {
                            recovered = frequency;
                            i = -99999999;
                        }
                        break;
                    case "jgz":
                        if (vx > 0) {
                            i += (int) vy;
                            i--;
                        }
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }

            Console.WriteLine("Recovered: {0}", recovered);
        }

        static void P2(string[] input)
        {
            long[][] reg = { new long[1 + 'z' - 'a'], new long[1 + 'z' - 'a']} ;
            reg[0]['p' - 'a'] = 0;
            reg[1]['p' - 'a'] = 1;

            int sends = 0;
            int current = 0;

            int[] i = new int[2];
            bool[] done = { false, false };
            bool[] waiting = { false, false };
            Queue<long>[] queue = { new Queue<long>(), new Queue<long>() };

            while (!done[0] && !done[1] && !(waiting[0] && waiting[1])) {
                for (; i[current] >= 0 && i[current] < input.Length; i[current]++) {
                    var parts = input[i[current]].Split();

                    long rx = parts[1][0];
                    long vx = -1;

                    if (rx >= 'a' && rx <= 'z') {
                        rx = rx - 'a';
                        vx = reg[current][rx];
                    } else {
                        vx = long.Parse(parts[1]);
                    }

                    long ry = -1;
                    long vy = -1;

                    if (parts.Length == 3) {
                        ry = parts[2][0];

                        if (ry >= 'a' && ry <= 'z') {
                            ry = ry - 'a';
                            vy = reg[current][ry];
                        } else {
                            vy = int.Parse(parts[2]);
                        }
                    }

                    switch (parts[0]) {
                        case "snd":
                            queue[(current + 1) % 2].Enqueue(vx);
                            waiting[(current + 1) % 2] = false;

                            if (current == 1) {
                                sends++;
                            }
                            break;
                        case "set":
                            reg[current][rx] = vy;
                            break;
                        case "add":
                            reg[current][rx] += vy;
                            break;
                        case "mul":
                            reg[current][rx] *= vy;
                            break;
                        case "mod":
                            reg[current][rx] %= vy;
                            break;
                        case "rcv":
                            if (queue[current].Count() > 0) {
                                reg[current][rx] = queue[current].Dequeue();
                            } else {
                                waiting[current] = true;
                            }
                            break;
                        case "jgz":
                            if (vx > 0) {
                                i[current] += (int) vy;
                                i[current]--;
                            }
                            break;
                        default:
                            throw new Exception();
                    }

                    if (waiting[current]) {
                        break;
                    }
                }

                if (!waiting[current]) {
                    done[current] = true;
                }
                current = (current + 1) % 2;
            }

            Console.WriteLine("Sends: {0}", sends);
        }
    }
}
