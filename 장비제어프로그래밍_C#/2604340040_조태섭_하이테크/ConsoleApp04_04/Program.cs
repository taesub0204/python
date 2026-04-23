using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp04_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] intArray = { 52, 273, 32, 65, 103 };

            Console.WriteLine(intArray[0]);
            Console.WriteLine("intArray의 길이는 "+intArray.Length);
            Console.WriteLine();

            intArray[0] = 0;


            for(int i =0; i < intArray.Length;i++)

            Console.WriteLine(intArray[i]);
            //Console.WriteLine(intArray[1]);
            //Console.WriteLine(intArray[2]);
            //Console.WriteLine(intArray[3]);
            //Console.WriteLine(intArray[4]);


        }
        //Console.WriteLine(intArray[5]); // out of range exception

            int j = 0;
            while (j < intArray.Length)
             {
            
                Console.WriteLine(intArray[j]);

            j++;
            
            }
    }
}
