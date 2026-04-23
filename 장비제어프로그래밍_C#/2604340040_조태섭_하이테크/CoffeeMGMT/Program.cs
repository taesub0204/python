using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMGMT
{
    internal class Program
    {
        static void Main(string[] args)   // ipconfig "/ all"  여기 입력 받는 곳이 string[]args이다. 
        {
            int result = 0;
            Console.WriteLine("Hellow word!");
            if (args.Length == 0)
            {
                Console.WriteLine("Main 함수에 입력 매개변수 없습니다");
            }
            if (args.Length == 1) {
                Console.WriteLine("Main 함수에 입력 매게변수 없습니다");
                Console.WriteLine("매개변수");
                if (args[0] == "/add") {
                    for (int i = 0; i <= 10; i++) 
                    {
                        result = result + i;
                    }
                    Console.WriteLine("1부터 10까지의 합은 " + result);
                }
            }
            Console.ReadLine();
        }
    }
}


// 1 ~ 100 까지 합을 구하는 프로그램 