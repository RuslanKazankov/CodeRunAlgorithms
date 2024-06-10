using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Problems
{
    public class LogicValue19 : ProblemBase
    {
        public override void Solve()
        {
            string input = Console.ReadLine()!;   //             1^0&!0&!1|(!(0|!1)&0^1) 

            Console.WriteLine(Execute(input));
        }

        private static string Execute(string s)
        {
            if (s.Contains('('))
            {
                int left = 0;
                int right = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(')
                    {
                        left = i;
                    }
                    if (s[i] == ')')
                    {
                        right = i;
                        break;
                    }
                }

                string start = s.Substring(0, left);
                string executable = Execute(s.Substring(left + 1, right - left - 1));
                string finish = s.Substring(right + 1);

                StringBuilder sb = new StringBuilder();
                sb.Append(start).Append(executable).Append(finish);
                return Execute(sb.ToString());
            }

            if (s.Contains('!'))
            {
                int left = s.IndexOf('!');
                int right = left + 1;
                char value = Ne(s[right]);
                s = s.Remove(left, 2);
                s = s.Insert(left, value.ToString());
                return Execute(s);
            }

            int indexOr = int.MaxValue;
            int indexAnd = int.MaxValue;
            int indexXor = int.MaxValue;

            if (s.Contains('|'))
            {
                indexOr = s.IndexOf('|');
            }
            if (s.Contains('&'))
            {
                indexAnd = s.IndexOf('&');
            }
            if (s.Contains('^'))
            {
                indexXor = s.IndexOf('^');
            }
            int minIndex = Math.Min(indexOr, Math.Min(indexAnd, indexXor));
            if (minIndex == int.MaxValue)
            {
                return s;
            }
            if (minIndex == indexOr)
            {
                int left = indexOr - 1;
                int right = left + 2;

                char value = Or(s[left], s[right]);

                s = s.Remove(left, 3);
                s = s.Insert(left, value.ToString());
                return Execute(s);
            }
            if (minIndex == indexAnd)
            {
                int left = indexAnd - 1;
                int right = left + 2;

                char value = And(s[left], s[right]);

                s = s.Remove(left, 3);
                s = s.Insert(left, value.ToString());
                return Execute(s);
            }
            if (minIndex == indexXor)
            {
                int left = indexXor - 1;
                int right = left + 2;

                char value = Xor(s[left], s[right]);

                s = s.Remove(left, 3);
                s = s.Insert(left, value.ToString());
                return Execute(s);
            }

            return s;
        }

        private static char Ne(char c)
        {
            if (c == '1')
            {
                return '0';
            }
            return '1';
        }

        private static char Or(char a, char b)
        {
            if (a == '1' || b == '1')
            {
                return '1';
            }
            return '0';
        }

        private static char And(char a, char b)
        {
            if (a == '1' && b == '1')
            {
                return '1';
            }
            return '0';
        }

        private static char Xor(char a, char b)
        {
            if (a == b)
            {
                return '0';
            }
            return '1';
        }
    }
}
