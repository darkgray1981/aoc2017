using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace d07
{
    class Program
    {

        private static string test1 = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(361527);

            // P2(361527);
            // P1(test1.Split("\r\n"));
            P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\r\n"));
            P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            var nodes = new Dictionary<string, Node>();
            

            Regex regex = new Regex(@"(?<name>[a-z]+) \((?<weight>[0-9]+)\)( -> (?<children>.+))?");

            string last = "";
            
            foreach (string line in input) {
                var m = regex.Match(line);
                // Console.WriteLine("Name: {0} [{1}] <> {2}", m.Groups["name"], m.Groups["weight"], m.Groups["children"]);

                var n = m.Groups["name"].Value;
                var w = int.Parse(m.Groups["weight"].Value);
                var cs = m.Groups["children"].Value;

                if (!nodes.ContainsKey(n)) {
                    nodes[n] = new Node(n);
                }

                nodes[n].weight = w;
                if (cs.Length > 0) {
                    nodes[n].children = cs.Split(", ");
                } else {
                    nodes[n].children = new string[0];
                }

                foreach (string c in nodes[n].children) {
                    if (!nodes.ContainsKey(c)) {
                        nodes[c] = new Node(c);
                    }
                    nodes[c].parent = n;
                }

                last = n;
            }

            Node root = nodes[last];
            while (root.parent != null) {
                root = nodes[root.parent];
            }

            Console.WriteLine("Root: {0}", root.name);
        }

        static void P2(string[] input)
        {
            var nodes = new Dictionary<string, Node>();
            

            Regex regex = new Regex(@"(?<name>[a-z]+) \((?<weight>[0-9]+)\)( -> (?<children>.+))?");

            string last = "";
            
            foreach (string line in input) {
                var m = regex.Match(line);
                // Console.WriteLine("Name: {0} [{1}] <> {2}", m.Groups["name"], m.Groups["weight"], m.Groups["children"]);

                var n = m.Groups["name"].Value;
                var w = int.Parse(m.Groups["weight"].Value);
                var cs = m.Groups["children"].Value;

                if (!nodes.ContainsKey(n)) {
                    nodes[n] = new Node(n);
                }

                nodes[n].weight = w;
                if (cs.Length > 0) {
                    nodes[n].children = cs.Split(", ");
                } else {
                    nodes[n].children = new string[0];
                }

                foreach (string c in nodes[n].children) {
                    if (!nodes.ContainsKey(c)) {
                        nodes[c] = new Node(c);
                    }
                    nodes[c].parent = n;
                }

                last = n;
            }

            Node root = nodes[last];
            while (root.parent != null) {
                root = nodes[root.parent];
            }

            TotalWeight(root, nodes);
        }

        private static int TotalWeight(Node n, Dictionary<string, Node> nodes)
        {
            int total = 0;

            var weightList = new List<int>();

            foreach (string c in n.children) {
                int treeWeight = TotalWeight(nodes[c], nodes);
                total += treeWeight;
                weightList.Add(treeWeight);
            }

            if (weightList.Count > 1) {
                weightList.Sort();
                if (weightList[0] != weightList[weightList.Count-1]) {
                    Console.WriteLine("{0} imbalanced!", n.name);

                    int correct = weightList[0];
                    if (weightList.Count > 2) {
                        correct = weightList[1];
                    }

                    total = 0;

                    foreach (string c in n.children) {
                        int treeWeight = TotalWeight(nodes[c], nodes);
                        if (treeWeight != correct) {
                            Console.WriteLine("{0}: {1} -> {2}", c, nodes[c].weight, nodes[c].weight + correct - treeWeight);
                            nodes[c].weight += correct - treeWeight;
                            treeWeight = TotalWeight(nodes[c], nodes);
                        }

                        total += treeWeight;
                    }
                }
            }

            return total + n.weight;
        }

        private class Node
        {
            public string name;
            public string parent;
            public int weight;
            public string[] children;

            public Node(string n)
            {
                name = n;
            }
        }
    }
}
