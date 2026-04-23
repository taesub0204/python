using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp02_59
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long Number = 2147483647L + 2147483647L; // 8byte 인데, 
            Console.WriteLine(Number);
            Console.WriteLine(Number/2);
            int intNumber = (int)Number;
            Console.WriteLine(intNumber);



            string numberString = "52273";
            //int intNumber2 = (int)numberString; // error 발생 
            int intNumber2 = int .Parse(numberString); // string을 int로 바꿔주는 함수  
            Console.WriteLine(intNumber2);
            Console.WriteLine();

            Console.WriteLine(float.Parse("52.273"));
            Console.WriteLine((777).ToString());
            Console.WriteLine((777).ToString().GetType());


            unchecked
            {
                Console.WriteLine(-(-2147483648));
             }

        }
    }
}

/*
 

**long**에 담긴 42억은 너무 커서 int 그릇에 다 안 들어간다.

그래서 앞부분은 버리고 뒷부분만 억지로 쑤셔 넣었다.

남은 뒷부분 비트 모양을 읽어보니 하필 그게 -2였다.

 */