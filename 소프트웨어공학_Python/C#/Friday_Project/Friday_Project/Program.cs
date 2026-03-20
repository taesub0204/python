using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friday_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("숫자를 입력하세요");
            int number = int.Parse(Console.ReadLine());
            int sum = 0;
            
            for (int i = 1;;i=i+2) //while(true)
            {
                
                if (i > number)
                    break;  // sum다 위에 넣어야 10까지 계산함, 반복되는 거를 막을 수 있음 
                sum += i;
                //if (i % 2 == 0)
                //    continue; // 아래 명령문 실행하지 말고 for로 가라
                //sum = sum + i;
            }
                Console.WriteLine($"1부터 {number}까지의 홀수의 합은 {sum}입니다.");
            



            //Console.WriteLine("숫자를 입력하세요");
            //int number = int.Parse(Console.ReadLine()); // 파이썬 동적 이라 괜찮은데 C# 정적 타이핑 변수 
            //                                            //int number1 = Convert.ToInt32(Console.ReadLine()); //


            //if (number > 0)
            //    //Console.WriteLine(number + "는 양수입니다.");
            //    Console.WriteLine($"{number}는(은) 양수입니다.");
            //else if(number < 0)
            //    Console.WriteLine($"{number}는(은) 음수입니다.");
            //else
            //    Console.WriteLine($"양수 0 입니다.");
            //// C언어도 정적 타이핑 변수다.









        }
    }
}
