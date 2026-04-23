using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp02_32
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("int:" + sizeof(int));
            Console.WriteLine("long:" + sizeof(long));
            Console.WriteLine("float:" + sizeof(float));
            Console.WriteLine("double:" + sizeof(double));
            Console.WriteLine("char:"+ sizeof(char));
            // 윈도우즈 64를 썻을 때 사이즈 확인
            //키보드 101키 아스키코드를 따름


            //Console.WriteLine("string" + sizeof(string)); 
            // string은  sizeof로 사이즈를 구할 수 없다.

            Console.WriteLine();
            bool one = 10 < 0;
            bool other = 20 > 100;
            Console.WriteLine(one);
            Console.WriteLine(other);
            Console.WriteLine(one.ToString());



            int number = 10;
            Console.WriteLine(number);
            Console.WriteLine(number++); // 후위 연산자
            Console.WriteLine(++number); // 11이 된다.
            Console.WriteLine(number--);
            Console.WriteLine(--number); // 9이 된다.

            Console.ReadLine();

        }
    }
}
