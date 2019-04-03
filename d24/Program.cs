using System;
using System.Collections.Generic;
using System.Linq;

namespace d24
{
    class Program
    {
        private static string test1 = @"0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\r\n"));
            P2alt(System.IO.File.ReadAllLines(@"input.txt"));

            // uh();

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var dic = new Dictionary<int, List<Component>>(input.Length);

            foreach (string line in input) {
                var parts = line.Split('/').Select(n => int.Parse(n)).ToArray();
                var c = new Component(parts[0], parts[1]);

                if (!dic.ContainsKey(parts[0])) {
                    dic[parts[0]] = new List<Component>();
                }
                if (!dic.ContainsKey(parts[1])) {
                    dic[parts[1]] = new List<Component>();
                }

                dic[parts[0]].Add(c);
                if (parts[0] != parts[1]) {
                    dic[parts[1]].Add(c);
                }
            }

            int strongest = 0;
            foreach (var c in dic[0]) {
                c.Plug(0);
                strongest = Math.Max(strongest, Connect(c, 0, dic));
                c.Unplug();
            }

            Console.WriteLine("Strongest: {0}", strongest);
        }

        private static int Connect(Component a, int strength, Dictionary<int, List<Component>> dic)
        {
            int strongest = strength;

            if (!a.usedA) {
                a.usedA = true;
                foreach (var b in dic[a.a]) {
                    if (b.Plug(a.a)) {
                        strongest = Math.Max(strongest, Connect(b, strength, dic));
                        b.Unplug();
                    }
                }
            } else if (!a.usedB) {
                a.usedB = true;
                foreach (var b in dic[a.b]) {
                    if (b.Plug(a.b)) {
                        strongest = Math.Max(strongest, Connect(b, strength, dic));
                        b.Unplug();
                    }
                }
            }
            
            return strongest + a.a + a.b;
        }

        static void P2(string[] input)
        {
            var dic = new Dictionary<int, List<Component>>(input.Length);

            foreach (string line in input) {
                var parts = line.Split('/').Select(n => int.Parse(n)).ToArray();
                var c = new Component(parts[0], parts[1]);

                if (!dic.ContainsKey(parts[0])) {
                    dic[parts[0]] = new List<Component>();
                }
                if (!dic.ContainsKey(parts[1])) {
                    dic[parts[1]] = new List<Component>();
                }

                dic[parts[0]].Add(c);
                if (parts[0] != parts[1]) {
                    dic[parts[1]].Add(c);
                }
            }

            var comb = new List<List<Component>>();

            foreach (var c in dic[0]) {
                c.Plug(0);
                Connect2(c, new List<Component>(), dic, comb);
                c.Unplug();
            }

            int strongest = 0;
            int longest = 0;
            foreach (var bridge in comb) {
                if (bridge.Count >= longest) {
                    longest = bridge.Count;

                    int strength = 0;
                    foreach (var c in bridge) {
                        strength += c.a + c.b;
                    }
                    if (strength > strongest) {
                        strongest = strength;
                    }
                }
            }

            Console.WriteLine("Strongest: {0} ({1})", strongest, comb.Count);
        }

        private static void Connect2(Component a, List<Component> path, Dictionary<int, List<Component>> dic, List<List<Component>> comb)
        {
            path.Add(a);

            bool plugged = false;

            if (!a.usedA) {
                a.usedA = true;
                foreach (var b in dic[a.a]) {
                    if (b.Plug(a.a)) {
                        plugged = true;
                        Connect2(b, path, dic, comb);
                        b.Unplug();
                    }
                }
            } else if (!a.usedB) {
                a.usedB = true;
                foreach (var b in dic[a.b]) {
                    if (b.Plug(a.b)) {
                        plugged = true;
                        Connect2(b, path, dic, comb);
                        b.Unplug();
                    }
                }
            }
            
            if (!plugged) {
                comb.Add(path.ToList());
                Console.WriteLine(string.Join("--", path));
            }

            path.RemoveAt(path.Count-1);
        }

        static void P2alt(string[] input)
        {
            var dic = new Dictionary<int, List<Component>>(input.Length);

            foreach (string line in input) {
                var parts = line.Split('/').Select(n => int.Parse(n)).ToArray();
                var c = new Component(parts[0], parts[1]);

                if (!dic.ContainsKey(parts[0])) {
                    dic[parts[0]] = new List<Component>();
                }
                if (!dic.ContainsKey(parts[1])) {
                    dic[parts[1]] = new List<Component>();
                }

                dic[parts[0]].Add(c);
                if (parts[0] != parts[1]) {
                    dic[parts[1]].Add(c);
                }
            }

            var best = (0, 0);
            foreach (var c in dic[0]) {
                c.Plug(0);
                best = Bestest(Connect3(c, (0, 0), dic), best);
                c.Unplug();
            }

            Console.WriteLine("Strongest: {0}", best);
        }


        private static (int, int) Connect3(Component a, (int, int) path, Dictionary<int, List<Component>> dic)
        {
            path.Item1++;
            path.Item2 += a.a + a.b;

            var best = path;

            if (!a.usedA) {
                a.usedA = true;
                foreach (var b in dic[a.a]) {
                    if (b.Plug(a.a)) {
                        best = Bestest(Connect3(b, path, dic), best);
                        b.Unplug();
                    }
                }
            } else if (!a.usedB) {
                a.usedB = true;
                foreach (var b in dic[a.b]) {
                    if (b.Plug(a.b)) {
                        best = Bestest(Connect3(b, path, dic), best);
                        b.Unplug();
                    }
                }
            }
            
            return best;
        }

        private static (int, int) Bestest((int, int) a, (int, int) b)
        {
            if (a.Item1 > b.Item1) {
                return a;
            }

            if (a.Item1 == b.Item1 && a.Item2 >= b.Item2) {
                return a;
            }

            return b;
        }

        private class Component
        {
            public int a;
            public int b;

            public bool usedA;
            public bool usedB;

            public Component(int aa, int bb)
            {
                a = aa;
                b = bb;
            }

            public bool Plug(int n)
            {
                if (!usedA && a == n) {
                    usedA = true;
                    return true;
                }

                if (!usedB && b == n) {
                    usedB = true;
                    return true;
                }

                return false;
            }

            public void Unplug()
            {
                usedA = usedB = false;
            }

            public override string ToString(){
                return a + "/" + b;
            }
        }
    }
}
