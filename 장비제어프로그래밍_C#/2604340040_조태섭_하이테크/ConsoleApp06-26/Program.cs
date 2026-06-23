using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp06_26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box(10,10);
            //box.width = -10; width가 private이므로 접근불가
            box.SetWidth(20); // width를 20으로 변경
            box.SetHeight(20);
            

            Console.WriteLine("Box width: " + box.GetWidth());
            Console.WriteLine("Box height: " + box.GetHeight());
            Console.WriteLine("Area: " + box.GetArea());


        }
    }
}
