using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp06_01
{
    internal class Program
    {
        class Test
        {
            public int Power(int x)
            {
                return x * x;
            }

            public int Muti(int x, int y)
            {
                return x * y;
            }

            public int Mutiply(int min, int max)
            {
                int output = 1;
                for (int i = min; i <= max; i++)
                {
                    output *= i;
                }
                return output;
            }
        }

        static void Main(string[] args)
        {
            Test test = new Test();
            Console.WriteLine(test.Power(10));
            Console.WriteLine(test.Power(20));
            Console.WriteLine();

            Console.WriteLine(test.Muti(52, 273));
            Console.WriteLine(test.Muti(10, 20));
            Console.WriteLine();

            Console.WriteLine(test.Mutiply(1, 10));
            Console.WriteLine();

            string[] newArgs = { "arg1", "arg2" };
            Main2(newArgs);
            TestCallMain2();

            // 프로그램이 바로 꺼지지 않고 결과를 눈으로 볼 수 있게 대기합니다.
            Console.WriteLine("\n아무 키나 누르면 프로그램이 종료됩니다...");
            Console.ReadKey();
        }

        static void Main2(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                Console.WriteLine("Main 함수 출력");
                foreach (var arg in args)
                {
                    // ⭕ {args}에서 {arg}로 수정하여 값이 올바르게 출력되도록 했습니다.
                    Console.WriteLine($"{arg}");
                }
            }
            else
            {
                Console.WriteLine("Main 함수에 전달된 인자가 없습니다.");
            }
        }

        static void TestCallMain2()
        {
            string[] newArgs = { "arg1", "arg2" };
            Main2(newArgs);
        }
    } // ⭕ Program 클래스 끝
} // ⭕ namespace 끝 (이 아래에 더 이상 아무것도 없어야 합니다!)