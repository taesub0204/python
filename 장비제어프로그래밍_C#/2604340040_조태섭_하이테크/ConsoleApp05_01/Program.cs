using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp05_01
{
    class FistClass
    {
        int i = 0;
    }
    class Second
    { 
        int i = 0;
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();
            Console.WriteLine(random.Next(1, 100)); // 
            Console.WriteLine(random.Next(1, 100));
            Console.WriteLine(random.Next(1, 100));
            Console.WriteLine(random.Next(1, 100));
            Console.WriteLine(random.Next(1, 100));
            Console.WriteLine(random.Next(1, 100));
            Console.WriteLine(random.NextDouble());
            Console.WriteLine(random.NextDouble()*10);

            //List<int> list = new List<int>();
            //list.Add(52);
            //list.Add(200);
            //list.Add(32);
            //list.Add(64);
            //list.Add(250);
            List<int> list = new List<int>() { 22,32,500,200};
            foreach (var item in list)
            {
                Console.WriteLine("Count : " + list.Count + "\t item :  "+item);
            }

            list.Remove(500);
            foreach (var item in list)
            {
                Console.WriteLine("Count : " + list.Count + "\t item :  " + item);
            }
            Console.WriteLine("Abs : " + Math.Abs(-234));
            Console.WriteLine("Ceiling: "+ Math.Ceiling(52.273)); // 53 정도
            Console.WriteLine("Floor: " + Math.Floor(52.273)); // 52 정도, 소수점 이하 버림, 정수 부분만 반환
            Console.WriteLine("Max : " + Math.Max(52, 237)); // 237 정도, 두 수 중에서 큰 수 반환
            Console.WriteLine("Min : " + Math.Min(52, 237)); // 52 정도, 두 수 중에서 작은 수 반환
            Console.WriteLine("Round : " +Math.Round(52.273)); // 52 정도, 0.5 이상이면 올림, 0.5 미만이면 내림

            //Product product = new Product(); // Product 클래스는 존재하지 않음, 컴파일 에러 발생
            // 단축키 Ctrl + . (점) : 클래스 생성, using문 추가 등 빠른 수정 가능

            //클래스를 생성하는 몇까지 방법
            /*
            1. 기존클래스에 새로운 클래스 생성
            2. 추가 별도의 클래스명.CS 파일 생성 후 클래스 생성
            3. Ctrl + . (점) : 클래스 생성, using문 추가 등 빠른 수정 가능

             
             
             
             */

        }
    }
}
