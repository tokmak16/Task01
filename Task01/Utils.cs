using System;
using System.Collections.Generic;
using System.Linq;

namespace Task01
{
    class Utils
    {
        int CheckNumbers(int[] list)
        {
            return list.Where(d => d > 0).Distinct().Count();
        }

        bool isPolindrome(string str)
        {
            var str2 = new string(str.Reverse().ToArray());
            return str2 == str;
        }
        bool isProperly(string str)
        {
            var stack = new Stack<char>();
            foreach (var bracket in str)
            {
                switch (bracket)
                {
                    case '(':
                        stack.Push(bracket);
                        break;
                    case ')':
                        if ((stack.Count == 0) || (stack.Pop() != '('))
                            return false;
                        break;
                }
            }
            return stack.Count == 0;
        }

        List<int> nominals(int amount)
        {
            var t = amount;
            var n = new int[] { 1, 5, 10, 20, 50 };

            var list = new List<int>();
            for (int i = n.Count() - 1; i >= 0; i--)
            {
                while (t >= n[i])
                {
                    t = t - n[i];
                    list.Add(n[i]);
                }
            }
            return list;
        }


        void Test()
        {
            int[] list = { 9, 7, 5, 1, 2, 9, 4, 0, -76, -5 };
            var count = CheckNumbers(list);

            var c1 = isPolindrome("ABC");
            var c2 = isPolindrome("DED");

            var d1 = isProperly("()(())(())(())(");

            var e1 = nominals(15);
            var e2 = string.Join(", ", e1);
        }
    }
}
