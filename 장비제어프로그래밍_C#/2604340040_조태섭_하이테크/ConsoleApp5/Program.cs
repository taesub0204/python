using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("학번, 이름, 나이, 전화번호, 주소, 과목1_점수, 과목2_점수, 과목3_점수을 입력하세요.");   
            string haknum = Console.ReadLine();
            string name = Console.ReadLine();
            string age = Console.ReadLine();
            string cellphone = Console.ReadLine();
            string addr = Console.ReadLine();

            uint sub1 = uint.Parse(Console.ReadLine());  // 형변환이 되지 않으면 연산이 되지 않음 
            //int sub1_1 = 100;
            int sub2 = int.Parse(Console.ReadLine()); 
            int sub3 = int.Parse(Console.ReadLine());
            int total =  (int)sub1 + sub2 + sub3;


            Console.WriteLine("-------------------------------");
            Console.Write("학번 : " + haknum);
            Console.Write(" 이름 : " + name);
            Console.Write("나이 : " + age);
            Console.Write("전화번호 : " + cellphone);
            Console.Write("주소 : " + addr);
            Console.Write("과목 1 :" + sub1);
            Console.Write("과목 2 :" + sub2);
            Console.Write("과목 3 :" + sub3);
            Console.Write("총점 : " + total);
            Console.WriteLine("평균 : " + total / 3.0);
            Console.WriteLine("-------------------------------");


        }
    }
}
