using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp03_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 학점 변수 
            Console.WriteLine("학점 입력 : ");
            double score = double.Parse(Console.ReadLine());

            if (score == 4.5)
                Console.WriteLine("A+");
            else if (4.2 <= score && score < 4.5)
                Console.WriteLine("A0");
            else if (3.5 <= score && score <4.2)
                Console.WriteLine("B+");
            else if (2.8 <= score && score < 3.5)
                Console.WriteLine("B0");
            else if (2.3 <= score && score < 2.8)
                Console.WriteLine("C+");
            else if (1.75 <= score && score < 2.3)
                Console.WriteLine("C0");
            else if (0.5 <= score && score < 1.0)
                Console.WriteLine("D+");
            else if (0 <= score && score < 0.5)
                Console.WriteLine("D0");
            else
                Console.WriteLine("F");

            Console.WriteLine();




            if (score == 4.5)
                Console.WriteLine("A+");
            else if (4.2 <= score )
                Console.WriteLine("A0");
            else if (3.5 <= score )
                Console.WriteLine("B+");
            else if (2.8 <= score )
                Console.WriteLine("B0");
            else if (2.3 <= score )
                Console.WriteLine("C+");
            else if (1.75 <= score )
                Console.WriteLine("C0");
            else if (0.5 <= score )
                Console.WriteLine("D+");
            else if (0 <= score )
                Console.WriteLine("D0");
            else
                Console.WriteLine("F");






        }
    }
}
