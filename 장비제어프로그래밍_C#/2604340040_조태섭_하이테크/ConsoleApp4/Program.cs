using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 273;
            int b = 52;

            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
            Console.WriteLine(a % b);

            int c = 2147483640;
            int d = 10;

            /*
            2,147,483,648 
            2,147,483,640 + 10 넘친 2만큼 음 수에서 2개 빠진 오버플로우 발생

            0

            -2,147,483,646
             
             
             */


            Console.WriteLine(c + d);
            Console.WriteLine(int.MaxValue);
            Console.WriteLine(int.MinValue);
            Console.WriteLine(long.MaxValue);
            Console.WriteLine(long.MinValue);



        }
    }
}
