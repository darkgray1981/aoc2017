using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace d20
{
    class Program
    {
        private static string test1 = @"p=<1,0,0>, v=<-1,0,0>, a=<-1,0,0>
p=<-1,0,0>, v=<-1,0,0>, a=<-1,0,0>";

        private static string test2 = @"p=<-10,0,0>, v=<-1,0,0>, a=<-1,0,0>
p=<-5,0,0>, v=<1,0,0>, a=<1,0,0>
p=<10,0,0>, v=<1,0,0>, a=<1,0,0>
p=<5,0,0>, v=<-1,0,0>, a=<-1,0,0>";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(test1.Split("\r\n"));
            P1alt(test1.Split("\r\n"));
            // P1(System.IO.File.ReadAllLines(@"input.txt"));
            P1alt(System.IO.File.ReadAllLines(@"input.txt"));

            P2(test2.Split("\r\n"));
            // P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            long[,] p = new long[input.Length,3];
            long[,] v = new long[input.Length,3];
            long[,] a = new long[input.Length,3];

            long[] closest = { -1, long.MaxValue };

            // p=<-833,-499,-1391>, v=<84,17,61>, a=<-4,1,1>

            Regex rx = new Regex(@"p=<(-?\d+),(-?\d+),(-?\d+)>, v=<(-?\d+),(-?\d+),(-?\d+)>, a=<(-?\d+),(-?\d+),(-?\d+)>");

            for (long i = 0; i < input.Length; i++) {
                var m = rx.Match(input[i]);

                p[i, 0] = long.Parse(m.Groups[1].Value);
                p[i, 1] = long.Parse(m.Groups[2].Value);
                p[i, 2] = long.Parse(m.Groups[3].Value);

                v[i, 0] = long.Parse(m.Groups[4].Value);
                v[i, 1] = long.Parse(m.Groups[5].Value);
                v[i, 2] = long.Parse(m.Groups[6].Value);

                a[i, 0] = long.Parse(m.Groups[7].Value);
                a[i, 1] = long.Parse(m.Groups[8].Value);
                a[i, 2] = long.Parse(m.Groups[9].Value);

                p[i, 0] += 1000000 * a[i, 0];
                p[i, 1] += 1000000 * a[i, 1];
                p[i, 2] += 1000000 * a[i, 2];


                long dist = Math.Abs(p[i,0]) + Math.Abs(p[i,1]) + Math.Abs(p[i,2]);
                if (dist < closest[1]) {
                    closest[0] = i;
                    closest[1] = dist;
                }
            }

            Console.WriteLine("Closest: {0} ({1})", closest[0], closest[1]);
        }


        static void P1alt(string[] input)
        {
            long[] p = new long[3];
            long[] v = new long[3];
            long[] a = new long[3];

            var particles = new Dictionary<int, Particle>();

            // p=<-833,-499,-1391>, v=<84,17,61>, a=<-4,1,1>

            Regex rx = new Regex(@"p=<(-?\d+),(-?\d+),(-?\d+)>, v=<(-?\d+),(-?\d+),(-?\d+)>, a=<(-?\d+),(-?\d+),(-?\d+)>");

            for (int i = 0; i < input.Length; i++) {
                var m = rx.Match(input[i]);

                p[0] = long.Parse(m.Groups[1].Value);
                p[1] = long.Parse(m.Groups[2].Value);
                p[2] = long.Parse(m.Groups[3].Value);

                v[0] = long.Parse(m.Groups[4].Value);
                v[1] = long.Parse(m.Groups[5].Value);
                v[2] = long.Parse(m.Groups[6].Value);

                a[0] = long.Parse(m.Groups[7].Value);
                a[1] = long.Parse(m.Groups[8].Value);
                a[2] = long.Parse(m.Groups[9].Value);

                particles[i] = new Particle(i, p.ToArray(), v.ToArray(), a.ToArray());
            }

            bool done = false;
            int count = 0;
            while (!done) {

                count++;

                done = true;

                foreach (Particle pt in particles.Values) {
                    pt.Move(10000);
                }

                foreach (Particle pa in particles.Values.ToList()) {
                    bool relevant = false;
                    foreach (Particle pb in particles.Values) {
                        if (pa.Nearing(pb)) {
                            relevant = true;
                            break;
                        }
                    }

                    if (relevant) {
                        done = false;
                        break;
                    }
                }
            }

            long[] closest = { -1, long.MaxValue };

            foreach (Particle pa in particles.Values) {
                long dist = pa.Dist();
                if (dist < closest[1]) {
                    closest[0] = pa.id;
                    closest[1] = dist;
                }
            }

            Console.WriteLine("Closest: {0} ({1}) after {2}", closest[0], closest[1], count);
        }


        static void P2(string[] input)
        {
            long[] p = new long[3];
            long[] v = new long[3];
            long[] a = new long[3];

            var particles = new Dictionary<int, Particle>();

            // p=<-833,-499,-1391>, v=<84,17,61>, a=<-4,1,1>

            Regex rx = new Regex(@"p=<(-?\d+),(-?\d+),(-?\d+)>, v=<(-?\d+),(-?\d+),(-?\d+)>, a=<(-?\d+),(-?\d+),(-?\d+)>");

            for (int i = 0; i < input.Length; i++) {
                var m = rx.Match(input[i]);

                p[0] = long.Parse(m.Groups[1].Value);
                p[1] = long.Parse(m.Groups[2].Value);
                p[2] = long.Parse(m.Groups[3].Value);

                v[0] = long.Parse(m.Groups[4].Value);
                v[1] = long.Parse(m.Groups[5].Value);
                v[2] = long.Parse(m.Groups[6].Value);

                a[0] = long.Parse(m.Groups[7].Value);
                a[1] = long.Parse(m.Groups[8].Value);
                a[2] = long.Parse(m.Groups[9].Value);

                particles[i] = new Particle(i, p.ToArray(), v.ToArray(), a.ToArray());
            }

            int count = 0;
            int loops = 0;

            while (particles.Count > 0) {
                loops++;

                var positions = new Dictionary<string, List<int>>();

                foreach (Particle pt in particles.Values) {
                    var pos = pt.Pos();
                    // Console.WriteLine("{0}: {1}", pt.id, pos);

                    if (!positions.ContainsKey(pos)) {
                        positions[pos] = new List<int>();
                    }
                    positions[pos].Add(pt.id);
                }

                if (positions.Count < particles.Count) {
                    foreach (var ps in positions.Values) {
                        if (ps.Count > 1) {
                            foreach (var id in ps) {
                                particles.Remove(id);
                            }
                        }
                    }
                }

                if (loops % 20 == 0) {
                        
                    foreach (Particle pa in particles.Values.ToList()) {
                        bool relevant = false;
                        foreach (Particle pb in particles.Values) {
                            if (pa.Nearing(pb)) {
                                relevant = true;
                                break;
                            }
                        }

                        if (!relevant) {
                            particles.Remove(pa.id);
                            count++;
                        }
                    }
                }

                foreach (Particle pt in particles.Values) {
                    pt.Move(1);
                }
            }

            Console.WriteLine("Left: {0} {1}", count, loops);
        }

        private class Particle
        {
            public int id;
            long[] p;
            long[] v;
            long[] a;

            public Particle(int n, long[] pp, long[] vv, long[] aa)
            {
                id = n;
                p = pp;
                v = vv;
                a = aa;
            }

            public void Move(long step)
            {
                v[0] += step * a[0];
                v[1] += step * a[1];
                v[2] += step * a[2];

                p[0] += step * v[0];
                p[1] += step * v[1];
                p[2] += step * v[2];
            }

            public void Back(long step)
            {
                p[0] -= step * v[0];
                p[1] -= step * v[1];
                p[2] -= step * v[2];

                v[0] -= step * a[0];
                v[1] -= step * a[1];
                v[2] -= step * a[2];
            }

            public string Pos()
            {
                return $"{p[0]},{p[1]},{p[2]}";
            }

            public long Dist()
            {
               return Math.Abs(p[0]) + Math.Abs(p[1]) + Math.Abs(p[2]);
            }

            public bool Nearing(Particle o)
            {
                long now = Math.Abs(p[0] - o.p[0]) + Math.Abs(p[1] - o.p[1]) + Math.Abs(p[2] - o.p[2]);

                this.Move(1);
                o.Move(1);

                long then = Math.Abs(p[0] - o.p[0]) + Math.Abs(p[1] - o.p[1]) + Math.Abs(p[2] - o.p[2]);

                this.Back(1);
                o.Back(1);

                return (now > then);
            }
        }
    }
}
