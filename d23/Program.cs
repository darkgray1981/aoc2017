using System;

namespace d23
{
    class Program
    {
        private static string test1 = @"ne,ne,s,s";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1);
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\n"));
            P2alt(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var reg = new long[1 + 'z' - 'a'];
            long multiplications = 0;

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
                    case "set":
                        reg[rx] = vy;
                        break;
                    case "sub":
                        reg[rx] -= vy;
                        break;
                    case "mul":
                        reg[rx] *= vy;
                        multiplications++;
                        break;
                    case "jnz":
                        if (vx != 0) {
                            i += (int) vy;
                            i--;
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine("Multiplications: {0}", multiplications);
        }

        static void P2(string[] input)
        {
            var reg = new long[1 + 'z' - 'a'];
            reg[0] = 1;

            long last = 0;

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
                    case "set":
                        reg[rx] = vy;
                        break;
                    case "sub":
                        reg[rx] -= vy;
                        if (rx == 'e' - 'a') {
                            if (vx == 2 && reg['d' - 'a'] != last && reg['b' - 'a'] % reg['d' - 'a'] == 0) {
                                reg['e' - 'a'] = reg['b' - 'a'] / reg['d' - 'a'];
                                last = reg['d' - 'a'];
                            } else {
                                reg['e' - 'a'] = reg['b' - 'a'];
                            }
                        }

                        if (rx == 'd' - 'a') {
                            if (vx * 2 > reg['b' - 'a']) {
                                reg['d' - 'a'] = reg['b' - 'a'];
                            }
                        }

                        break;
                    case "mul":
                        reg[rx] *= vy;
                        break;
                    case "jnz":
                        if (vx != 0) {
                            i += (int) vy;
                            i--;
                        }
                        break;
                    default:
                        throw new Exception();
                }

                if (rx == 'h' - 'a') {
                    Console.WriteLine("[ {0} ]", string.Join(" ", reg));
                }

                // System.Threading.Thread.Sleep(50);
                // Console.WriteLine("{0:D4}: [ {1} ] {2}", i+1, string.Join(" ", reg), rx == 'e'-'a');

            }

            Console.WriteLine("H value: {0}", reg['h' - 'a']);
        }

        static void P2alt(string[] input)
        {
            var reg = new long[1 + 'z' - 'a'];
            reg[0] = 1;

            long last = 0;

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
                    case "set":
                        reg[rx] = vy;
                        break;
                    case "sub":
                        reg[rx] -= vy;
                        break;
                    case "mul":
                        reg[rx] *= vy;
                        break;
                    case "jnz":
                        if (vx != 0) {
                            i += (int) vy;
                            i--;
                        }
                        break;
                    default:
                        throw new Exception();
                }

                if (i == 9) {
                    break;
                }
            }

            Console.WriteLine("H value: {0}", NonPrimes(reg['b' - 'a'], reg['c' - 'a'], 17));
        }

        private static int NonPrimes(long start, long end, long step)
        {
            int count = 0;
            
            for (long i = start; i <= end; i += step) {
                if (i % 2 == 0) {
                    count++;
                } else {
                    for (long n = 3; n * n <= i; n += 2) {
                        if (i % n == 0) {
                            count++;
                            break;
                        }
                    }
                }
            }

            return count;
        }
    }
}
