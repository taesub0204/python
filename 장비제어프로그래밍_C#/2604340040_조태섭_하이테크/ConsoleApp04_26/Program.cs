using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp04_26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "Potato Tomato";
            Console.WriteLine("대문자로 : "+input.ToUpper());
            Console.WriteLine("소문자로 : "+input.ToLower());
            Console.WriteLine(input);
        }
    }
}
