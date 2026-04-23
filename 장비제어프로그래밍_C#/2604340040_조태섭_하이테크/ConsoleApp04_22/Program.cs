using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp04_22
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("숫자를 입력해주세요(짝수를 입력하면 종료): ");
                int input = int.Parse(Console.ReadLine());

                if (input % 2 == 0)
                {
                    break;
                }


            }
        }
    }
}
