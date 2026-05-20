using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp05_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = new List<int>();

            // 리스트에 요소 추가

            //list.Add(52);
            //list.Add(273);
            //list.Add(32);
            //list.Add(64);

            List<int> list = new List<int> { 52, 273, 32, 64 };

            // 반복 수행
            foreach (var item in list)
            {
                Console.WriteLine("Count :" + list.Count + "\titem:" + item);
            }
            Console.WriteLine();
            list.Remove(273); // 요소 제거
            foreach (var item in list)
            {
                Console.WriteLine("Count :" + list.Count + "\titem:" + item);
            }
            Console.WriteLine();

            if (list.Count != 0)
            { 
            foreach (var item in list)
            {
                Console.WriteLine("Count :" + list.Count + "\titem:" + item);
            }
            }
            else
            {
                Console.WriteLine("객체에 데이터가 없습니다.");
            }






            //list.RemoveAll(n => n > 50);
            list.Clear(); // 모든 요소 제거

            Console.WriteLine();
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine("Count :" + list.Count + "\titem:" + item);
                }
            }
            else
            {
                Console.WriteLine("객체에 데이터가 없습니다.");
            }


            Console.WriteLine(Math.Abs(-52273)); // 절대값
            Console.WriteLine(Math.Ceiling(52.273)); // 올림
            Console.WriteLine(Math.Floor(52.273)); // 내림
            Console.WriteLine(Math.Max(52, 273)); // 최대값
            Console.WriteLine(Math.Min(52, 273)); // 최소값
            Console.WriteLine(Math.Round(52.273)); // 반올림





        }
    }
}
