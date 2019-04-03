using System;
using System.Collections.Generic;
using System.Linq;

namespace d25
{
    class Program
    {
        private static string test1 = @"Begin in state A.
Perform a diagnostic checksum after 6 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var sw = System.Diagnostics.Stopwatch.StartNew();

            // P1(test1.Split("\r\n\r\n"));
            // P1alt(System.IO.File.ReadAllText(@"input.txt").Split("\n\n"));
            P1altX(System.IO.File.ReadAllText(@"input.txt").Split("\n\n"));

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

        static void P1(string[] input)
        {
            char state = input[0].Split("\n")[0].Split()[3][0];
            int pos = int.Parse(input[0].Split("\n")[1].Split()[5]);
            char[] tape = new char[2*pos+1];

            var states = new Dictionary<char, State>();

            for (int i = 1; i < input.Length; i++) {

                var lines = input[i].Split("\n");

                State s;

                s.Name = lines[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[2][0];
                s.FalseWrite = lines[2].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.FalseMove = (lines[3].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.FalseNext = lines[4].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueWrite = lines[6].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueMove = (lines[7].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.TrueNext = lines[8].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];

                states[s.Name] = s;
            }

            int checksum = 0;
            int rounds = pos;
            for (int i = 0; i < rounds; i++) {
                char value = tape[pos];
                var s = states[state];

                if (value == '1') {
                    tape[pos] = s.TrueWrite;
                    pos += s.TrueMove;
                    state = s.TrueNext;

                    if (s.TrueWrite != '1') {
                        checksum--;
                    }

                } else {
                    tape[pos] = s.FalseWrite;
                    pos += s.FalseMove;
                    state = s.FalseNext;

                    if (s.FalseWrite == '1') {
                        checksum++;
                    }
                }
            }

            Console.WriteLine("Checksum: {0}", checksum);
        }


        static void P1alt(string[] input)
        {
            char state = input[0].Split("\n")[0].Split()[3][0];
            int pos = int.Parse(input[0].Split("\n")[1].Split()[5]);
            char[] tape = new char[2*pos+1];

            var states = new State[input.Length - 1];

            for (int i = 1; i < input.Length; i++) {

                var lines = input[i].Split("\n");

                State s;

                s.Name = lines[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[2][0];
                s.FalseWrite = lines[2].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.FalseMove = (lines[3].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.FalseNext = lines[4].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueWrite = lines[6].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueMove = (lines[7].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.TrueNext = lines[8].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];

                states[s.Name - 'A'] = s;
            }

            int checksum = 0;
            int rounds = pos;
            for (int i = 0; i < rounds; i++) {
                char value = tape[pos];
                var s = states[state - 'A'];

                if (value == '1') {
                    tape[pos] = s.TrueWrite;
                    pos += s.TrueMove;
                    state = s.TrueNext;

                    if (s.TrueWrite != '1') {
                        checksum--;
                    }

                } else {
                    tape[pos] = s.FalseWrite;
                    pos += s.FalseMove;
                    state = s.FalseNext;

                    if (s.FalseWrite == '1') {
                        checksum++;
                    }
                }
            }

            Console.WriteLine("Checksum: {0}", checksum);
        }

        static void P1altX(string[] input)
        {
            char state = input[0].Split("\n")[0].Split()[3][0];
            int rounds = int.Parse(input[0].Split("\n")[1].Split()[5]);
            char[] tape = new char[20000];
            int pos = tape.Length/2;

            var states = new State[input.Length - 1];

            for (int i = 1; i < input.Length; i++) {

                var lines = input[i].Split("\n");

                State s;

                s.Name = lines[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[2][0];
                s.FalseWrite = lines[2].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.FalseMove = (lines[3].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.FalseNext = lines[4].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueWrite = lines[6].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];
                s.TrueMove = (lines[7].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6][0] == 'r' ? 1 : -1);
                s.TrueNext = lines[8].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];

                states[s.Name - 'A'] = s;
            }

            int checksum = 0;
            for (int i = 0; i < rounds; i++) {
                char value;
                try {
                    value = tape[pos];
                } catch (IndexOutOfRangeException) {
                    var temp = new char[tape.Length * 4 / 3];
                    tape.CopyTo(temp, temp.Length / 10);
                    pos += temp.Length / 10;
                    tape = temp;
                    value = tape[pos];
                }

                var s = states[state - 'A'];

                if (value == '1') {
                    tape[pos] = s.TrueWrite;
                    pos += s.TrueMove;
                    state = s.TrueNext;

                    if (s.TrueWrite != '1') {
                        checksum--;
                    }

                } else {
                    tape[pos] = s.FalseWrite;
                    pos += s.FalseMove;
                    state = s.FalseNext;

                    if (s.FalseWrite == '1') {
                        checksum++;
                    }
                }
            }

            Console.WriteLine("Checksum: {0}", checksum);
        }
        struct State
        {
            public char Name;
            public char FalseWrite;
            public int FalseMove;
            public char FalseNext;

            public char TrueWrite;
            public int TrueMove;            
            public char TrueNext;
        }
    }
}
