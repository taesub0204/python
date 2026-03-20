using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Friday_Project
{
    internal class Program1
    {
        static void Main(string[] args) //메인도 함수 C# 컴파일러에서 자동호출되게 되어 있음
        {

           Console.WriteLine(Hello("김철수"));
           Console.WriteLine(Hello("박지수"));
        
           string result = Hello("이민호");
           Console.WriteLine($"신입생 인사 : {result}");

           Console.WriteLine(Add(1, 2));
           //Console.WriteLine(Sub(1, 2));
           Console.WriteLine(Add(10, 5));
           //Console.WriteLine(Sub(10, 5));
           Sub(10, 5);



        }

        static string Add(int a, int b) { 
            int result = a + b;
            return $"두 수의 합은 {result} 입니다.";
        }
        static void Sub(int a, int b) // 반환하지 않으면 
        {
            int result = a - b;
            Console.WriteLine($"두 수의 차은 {result} 입니다.");
          
        }



        static string Hello(string name)
            {
            string insa = "제 이름은" + name + "입니다. 만나서 반갑습니다.";


                return (insa);

            }
        
        // 함수는 자주 사용하는 기능을 함수로 만들어서  writeline(), Hello()




    }
}
