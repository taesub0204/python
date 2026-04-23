using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp03_03
{
    internal class Program
    {
        static void Main(string[] args)
        {




            Console.Write(DateTime.Now.Year + "년도 ");
            Console.Write(DateTime.Now.Month + "월 ");
            Console.Write(DateTime.Now.Day + "일 ");
            if (DateTime.Now.Hour < 12)
            {
                Console.Write("AM ");
            }
            if (DateTime.Now.Hour >= 12)
            {
                Console.Write("PM ");
                Console.Write(DateTime.Now.Hour - 12 + "시 ");
            }

            Console.Write(DateTime.Now.Minute + "분 ");
            Console.Write(DateTime.Now.Second + "초 ");

            if (DateTime.Now.Hour < 12)
            {
                Console.WriteLine("오전입니다. ");
            }
            if (DateTime.Now.Hour >= 12)
            {
                Console.WriteLine("오후입니다. ");

            }




            Console.WriteLine("숫자입력: ");
            int input = int.Parse(Console.ReadLine());

            if (input % 2 == 0) // 둘다 플로우상 수행됨 
            {
                Console.WriteLine("짝수입니다.");
            }
            else
            {
                Console.WriteLine("홀수입니다."); // 홀수입니다. 수행됨  flow chat 그리는 게 좋음
            }


            Console.WriteLine();
            // 중첩 조건문


            if (DateTime.Now.Hour < 12)
            {
                Console.WriteLine("오전입수업니다. ");

            }

               else if (DateTime.Now.Hour <= 16)
                {
                    Console.WriteLine("오후 수업입니다. ");
                }
                else
                {
                    Console.WriteLine("오후 수업 종료 되었습니다. ");
                }



            
        }
    }
    
}
