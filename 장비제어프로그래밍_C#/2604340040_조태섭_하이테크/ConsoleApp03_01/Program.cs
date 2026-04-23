using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp03_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("숫자입력: ");
            int input = int.Parse(Console.ReadLine());

            if (input % 2 == 0) // 둘다 플로우상 수행됨 
            {
                Console.WriteLine("짝수입니다.");
            }
            if (input % 2 == 1)
            {
                Console.WriteLine("홀수입니다."); // 홀수입니다. 수행됨  flow chat 그리는 게 좋음

            }
        }
    }
}
