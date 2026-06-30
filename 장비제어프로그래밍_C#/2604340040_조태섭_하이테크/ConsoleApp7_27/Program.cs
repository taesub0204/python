using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7_26
{
    internal class Program
    {
        class Parent
        {
            public int variable = 273;
            public virtual void Method()
            {
                Console.WriteLine("부모의 메서드");
            }
        }

        class Child : Parent
        {
            public new String variable = "hiding";
            public override void Method()
            {
                Console.WriteLine("자식의 메서드");
            }
        }

        static void Main(string[] args)
        {

            Child child = new Child();

            child.Method();
            Console.WriteLine(child.variable);

            ((Parent)child).Method();
            //Console.WriteLine(((Parent)child).variable);




        }
    }
}
