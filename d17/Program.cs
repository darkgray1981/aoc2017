using System;

namespace d17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            P1(348, 2017);
            P2(348, 50000000);

            // P2(361527);
            // P1(test1.Split("\r\n"));
            // P1(System.IO.File.ReadAllLines(@"input.txt"));

            // P2(test1.Split("\r\n"));
            // P2(System.IO.File.ReadAllLines(@"input.txt"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(int step, int loops)
        {
            var node = new Node(0, null);
            node.next = node;

            var initial = node;

            for (int n = 1; n <= loops; n++) {

                for (int i = 0; i < step % n; i++) {
                    node = node.next;
                }

                Node inserted = new Node(n, node.next);
                node.next = inserted;
                node = inserted;

                // Node foo = initial;
                // do {
                //     Console.Write("{0} ", foo.data);
                //     foo = foo.next;
                // } while (foo != initial);

                // Console.WriteLine();
            }

            Console.WriteLine("After: {0}", node.next.data);
        }

        static void P2(int step, int loops)
        {
            var node = new Node(0, null);
            node.next = node;

            var initial = node;
            int val = 0;
            int pos = 0;

            for (int n = 1; n <= loops; n++) {
                pos = (pos + step) % n;
                pos++;
                if (pos == 1) {
                    val = n;
                }
            }

            Console.WriteLine("After: {0}", val);
        }
        private class Node
        {
            public int data;
            public Node next;

            public Node(int d, Node n)
            {
                data = d;
                next = n;
            }
        }
    }
}
