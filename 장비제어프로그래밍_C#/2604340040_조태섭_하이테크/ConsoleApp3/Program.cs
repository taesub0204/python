using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    /*
     * 클래스명 : Program
     * 맴버:
     * 메서드:
     * 요약:
     * 개발자:
   */



    internal class Program
    {
        static void Main(string[] args)
            
        {
            int result = 0; //  결과 값 누적을 위한 변수 선언
            string process = "";
            for (int i = 1; i < 101; i++) // 1~100까지 반복하면서 결과 값 누적 및 과정 문자열 생성  
            {
                result = result + i;
                if (i == 1)
                    process = process + i;
                else
                    process = process + "+" + i;
            }
                Console.WriteLine(process+"="+result); // 결과값 출력

            /*
    

            */
            Console.WriteLine("1~100까지 합을 구하는 프로그램"+result);
            Console.WriteLine("WriteLine");
            Console.Write("Write");
            Console.Write("Write");
            Console.Write("Write");
            Console.Write("Write");
            Console.WriteLine("WriteLine");
            Console.WriteLine("WriteLine");
        }
    }
}
