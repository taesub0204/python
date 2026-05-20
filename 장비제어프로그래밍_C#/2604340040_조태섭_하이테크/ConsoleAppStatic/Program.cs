using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStatic
{
    static class Utility // 1. static 클래스 :  상속할 수 없고, 인스턴스, 인스턴스 메서드, 생성자 정의할 없음
    {
        // 2. static 변수 (모든 인스턴스가 공유)
        public static int Counter = 0;
        //public int cnt; // 2. static 클래스 내에서는 인스턴스 변수 정의 불가 (컴파일 에러)

        //3. static 메서드 (인스턴스 없이 호출 가능)
        public static void PrintMessage(string msg) {
            Console.WriteLine("Utility"+msg);
        }
        //public void MSG()
        //{ 
        //    Console.WriteLine("Utility"+Counter); // 2. static 클래스 내에서는 인스턴스 메서드 정의 불가 (컴파일 에러)
        //}
    }

    class MyMath
    {
        public int cnt;

        public void MyMsg()
        {
            Console.WriteLine("MyMsg Method");
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            Utility.Counter++; // 3. static 변수 : 클래스 이름으로 직접 접근, 인스턴스 필요 없음
            Utility.Counter++; // 3. static 변수 : 클래스 이름으로 직접 접근, 인스턴스 필요 없음
            Console.WriteLine("Counter: " + Utility.Counter); // 3. static 변수 : 클래스 이름으로 직접 접근, 인스턴스 필요 없음


            

            Utility.PrintMessage("Hello"); // 3. static 메서드 : 클래스 이름으로 직접 호출, 인스턴스 필요 없음
            //Utility utility = new Utility(); // 4. static 클래스는 인스턴스 생성 불가 (컴파일 에러)

        }
    }
}
