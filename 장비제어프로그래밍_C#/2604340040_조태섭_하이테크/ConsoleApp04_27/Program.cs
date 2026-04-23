using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp04_27
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "감자 고구마 토마토";
            string[] inputs = input.Split(new char[] { '고' });

            foreach (var item in inputs)
            { 
                Console.WriteLine(item);
            }
            //for (int i = 0; i < 3; i++)
            //{ 
            //    Console.WriteLine(inputs[i]);
            //}

            string inp = "         test       \n";
            Console.WriteLine("::"+inp.Trim()+"::");
            Console.WriteLine("::" + inp.TrimStart() + "::");
            Console.WriteLine("::" + inp.TrimEnd() + "::");

            Thread.Sleep(10000);
            Console.Clear();
            Console.Write("메서드 호출 전");
            Console.SetCursorPosition(5, 5);
            Console.Write("메서드 호출 후");


        }
    }
}
