using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7_18
{
    internal class Program
    {
        class Parent
        {

            public Parent() { Console.WriteLine("부모 생성자:: Parent( )"); }
            public Parent(int param) { Console.WriteLine("부모 생성자:: Parent(int Param)"); }
            public Parent(String param) { Console.WriteLine("부모 생성자:: Parent(stirng Param)"); }

        }
        class Child : Parent
        {
            public Child() :base()
                {
                    Console.WriteLine("자식 생성자:: Child():base");
                 }
            //public Child() { Console.WriteLine("자식 생성자"); }
            public Child(int input) : base(input)
            {
                Console.WriteLine("자식 생성자:: Child(int input):base(input)");
            }
            public Child(String input) : base(input)
            {
                Console.WriteLine("자식 생성자:: Child(stirng Param):base(input)");
            }


        }




        static void Main(string[] args)
        {
            Child child = new Child();
            Child child2 = new Child(10);
            Child child3 = new Child("문자열");


        }
    }
}
