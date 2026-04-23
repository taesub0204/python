using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp04_23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++) 
            { 
                Console.WriteLine("외부 반복문");
                for (int j = 0; j < 10; j++)
                { 
                    Console.WriteLine("내부 반복문");
                    //goto doNotUse;

                }


            }

            //doNotUse:
            // Console.WriteLine("goto문을 사용하여 반복문을 탈출했습니다.");

        }
    }
}
