using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp04_21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cnt = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    Console.Write("*");
                    cnt++;
                }
                Console.WriteLine('\n');

            }

            Console.WriteLine("반복횟수 " + cnt);

        }

    }
}
// 성능에 민감 한 프로그래밍은  중첩해서 사용하지 않는 것이 좋다. for
