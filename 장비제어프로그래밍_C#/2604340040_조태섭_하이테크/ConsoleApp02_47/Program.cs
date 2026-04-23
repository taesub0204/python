using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp02_47
{
    internal class Program
    {
        // 전역변수 위치
        //var number = 0;
        static void Main(string[] args)
        {// 지역변수 위치
            int _int = 273;
            long _long = 522731033265;
            float _float = 52.273f;
            double _double = 52.273;
            char _char = '글';
            string _string = "문자열";

            var num = 0;
            //var num2;
            var num1 = 0;
            string input = " ";


            Console.WriteLine(_int.GetType()); 
            Console.WriteLine(_long.GetType());
            Console.WriteLine(_float.GetType());    
            Console.WriteLine(_double.GetType());
            Console.WriteLine(_char.GetType());
            Console.WriteLine(_string.GetType());   
            Console.WriteLine();

            num1 = add(10);
            Console.WriteLine(num1);

            num1 = add(100);
            Console.WriteLine(num1);
            Console.ReadLine();
            Console.WriteLine("input"+ input);



        }
        static int add(int a) {
            //지역변수 위치
            var result = 0;

            for (int i = 0; i <= a; i++) { 
                result = result + i;
            }
        
        
            return result;
        }



    }
}
