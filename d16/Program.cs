using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace d16
{
    class Program
    {
        private static string test1 = @"s1,x3/4,pe/b";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1, 5);
            P1(System.IO.File.ReadAllText(@"input.txt"), 16);

            // P2(test1.Split("\r\n"));
            // P2alt(test1, 5);
            P2altX(System.IO.File.ReadAllText(@"input.txt"), 16);

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string input, int size)
        {
            var arr = new char[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)('a' + i);
            }

            int start = 0;
            foreach (string step in input.Split(','))
            {
                char move = step[0];
                string[] parts = step.Substring(1).Split('/');
                char temp;

                switch (move)
                {
                    case 's':
                        int x = int.Parse(step.Substring(1));
                        start = (10000 * size + start - x) % size;
                        break;
                    case 'x':
                        int xa = (start + int.Parse(parts[0])) % arr.Length;
                        int xb = (start + int.Parse(parts[1])) % arr.Length;

                        temp = arr[xa];
                        arr[xa] = arr[xb];
                        arr[xb] = temp;
                        break;
                    case 'p':
                        char pa = parts[0][0];
                        char pb = parts[1][0];

                        int idxA = Array.IndexOf(arr, pa);
                        int idxB = Array.IndexOf(arr, pb);

                        temp = arr[idxA];
                        arr[idxA] = arr[idxB];
                        arr[idxB] = temp;
                        break;
                    default:
                        break;
                }
            }

            var sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[(start + i) % arr.Length]);
            }

            Console.WriteLine("Order: {0}", sb);
        }

        static void P2(string input, int size)
        {
            var arr = new char[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)('a' + i);
            }

            var steps = input.Split(',');

            var seen = new Dictionary<string, int[]>();

            int start = 0;
            int iter = 0;

            while (iter < 1000000000) {

                if (!seen.ContainsKey(new string(arr))) {
                    seen[new string(arr)] = new int[]{iter, start};
                } else {
                    int last = seen[new string(arr)][0];
                    int period = iter - last;
                    int desired = last + (1000000000 - iter) % period;

                    foreach (var e in seen) {
                        if (e.Value[0] == desired) {
                            arr = e.Key.ToCharArray();
                            start = e.Value[1];
                            break;
                        }
                    }

                    break;
                }

                foreach (string step in steps)
                {
                    char move = step[0];
                    string[] parts = step.Substring(1).Split('/');
                    char temp;

                    switch (move)
                    {
                        case 's':
                            int x = int.Parse(step.Substring(1));
                            start = (10000 * size + start - x) % size;
                            break;
                        case 'x':
                            int xa = (start + int.Parse(parts[0])) % size;
                            int xb = (start + int.Parse(parts[1])) % size;

                            temp = arr[xa];
                            arr[xa] = arr[xb];
                            arr[xb] = temp;
                            break;
                        case 'p':
                            char pa = parts[0][0];
                            char pb = parts[1][0];

                            int idxA = Array.IndexOf(arr, pa);
                            int idxB = Array.IndexOf(arr, pb);

                            temp = arr[idxA];
                            arr[idxA] = arr[idxB];
                            arr[idxB] = temp;
                            break;
                        default:
                            break;
                    }
                }

                iter++;
            }

            // Console.WriteLine("Repetition: {0} at {1}", new string(arr), iter);

            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(arr[(start + i) % size]);
            }

            Console.WriteLine("Order: {0}", sb);
        }


        static void P2alt(string input, int size)
        {
            var arr = new char[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)('a' + i);
            }

            var dance = new List<Move>();

            foreach (string step in input.Split(','))
            {
                Move move = new Move(step[0]);
                string[] parts = step.Substring(1).Split('/');

                switch (move.name)
                {
                    case 's':
                        move.shift = int.Parse(step.Substring(1));
                        break;
                    case 'x':
                        move.ia = int.Parse(parts[0]);
                        move.ib = int.Parse(parts[1]);
                        break;
                    case 'p':
                        move.ca = parts[0][0];
                        move.cb = parts[1][0];
                        break;
                    default:
                        break;
                }

                dance.Add(move);
            }

            var seen = new Dictionary<string, int[]>();

            int start = 0;
            int iter = 0;

            while (iter < 1000000000) {

                if (!seen.ContainsKey(new string(arr))) {
                    seen[new string(arr)] = new int[]{iter, start};
                } else {
                    int last = seen[new string(arr)][0];
                    int period = iter - last;
                    int desired = last + (1000000000 - iter) % period;

                    foreach (var e in seen) {
                        if (e.Value[0] == desired) {
                            arr = e.Key.ToCharArray();
                            start = e.Value[1];
                            break;
                        }
                    }

                    break;
                }

                foreach (Move step in dance)
                {
                    char temp;

                    switch (step.name)
                    {
                        case 's':
                            start = (((start - step.shift) % size) + size) % size;
                            break;
                        case 'x':
                            int xa = (start + step.ia) % size;
                            int xb = (start + step.ib) % size;

                            temp = arr[xa];
                            arr[xa] = arr[xb];
                            arr[xb] = temp;
                            break;
                        case 'p':
                            int idxA = Array.IndexOf(arr, step.ca);
                            int idxB = Array.IndexOf(arr, step.cb);

                            temp = arr[idxA];
                            arr[idxA] = arr[idxB];
                            arr[idxB] = temp;
                            break;
                        default:
                            break;
                    }
                }

                iter++;
            }

            // Console.WriteLine("Repetition: {0} at {1}", new string(arr), iter);

            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(arr[(start + i) % size]);
            }

            Console.WriteLine("Order: {0}", sb);
        }

        static void P2altX(string input, int size)
        {
            var arr = new char[size];
            var dancers = new int[size];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (char)('a' + i);
                dancers[i] = i;
            }

            var dance = new List<Move>();

            foreach (string step in input.Split(','))
            {
                Move move = new Move(step[0]);
                string[] parts = step.Substring(1).Split('/');

                switch (move.name)
                {
                    case 's':
                        move.shift = int.Parse(step.Substring(1));
                        break;
                    case 'x':
                        move.ia = int.Parse(parts[0]);
                        move.ib = int.Parse(parts[1]);
                        break;
                    case 'p':
                        move.ca = parts[0][0];
                        move.cb = parts[1][0];
                        break;
                    default:
                        break;
                }

                dance.Add(move);
            }

            var seen = new Dictionary<string, int[]>();

            int start = 0;
            int iter = 0;

            while (iter < 1000000000) {

                if (!seen.ContainsKey(new string(arr))) {
                    seen[new string(arr)] = new int[]{iter, start};
                } else {
                    int last = seen[new string(arr)][0];
                    int period = iter - last;
                    int desired = last + (1000000000 - iter) % period;

                    foreach (var e in seen) {
                        if (e.Value[0] == desired) {
                            arr = e.Key.ToCharArray();
                            start = e.Value[1];
                            break;
                        }
                    }

                    break;
                }

                foreach (Move step in dance)
                {
                    char temp;

                    switch (step.name)
                    {
                        case 's':
                            start = (((start - step.shift) % size) + size) % size;
                            break;
                        case 'x':
                            int xa = (start + step.ia) % size;
                            int xb = (start + step.ib) % size;

                            temp = arr[xa];
                            arr[xa] = arr[xb];
                            arr[xb] = temp;

                            dancers[arr[xa] - 'a'] = xa;
                            dancers[arr[xb] - 'a'] = xb;
                            break;
                        case 'p':
                            int idxA = dancers[step.ca - 'a'];
                            int idxB = dancers[step.cb - 'a'];

                            temp = arr[idxA];
                            arr[idxA] = arr[idxB];
                            arr[idxB] = temp;

                            dancers[step.ca - 'a'] = idxB;
                            dancers[step.cb - 'a'] = idxA;
                            break;
                        default:
                            break;
                    }
                }

                iter++;
            }

            // Console.WriteLine("Repetition: {0} at {1}", new string(arr), iter);

            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(arr[(start + i) % size]);
            }

            Console.WriteLine("Order: {0}", sb);
        }

        private class Move {

            public char name;
            public int shift;
            public int ia;
            public int ib;
            public char ca;
            public char cb;

            public Move(char n) {
                name = n;
            }
        }

    }
}
