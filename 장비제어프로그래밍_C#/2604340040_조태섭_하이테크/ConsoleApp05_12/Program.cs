using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp05_12
{
    internal class Program
    {
        //class Product
        //{
        //    public string name { get; set; }
        //    public int price { get; set; }
        //}
        class MyMath
        { 
            public static double PI = 3.14;

        }


        static void Main(string[] args)
        {
            Product product1 = new Product() { name = "감자", price = 2000};
            Product product2 = new Product() { name = "고구마", price = 3000 };
            //product.name = "감자";
            //product.price = 2000;

            Console.WriteLine(product1.name + ":" + product1.price + "원");
            Console.WriteLine(product2.name + ":" + product2.price + "원");
            Console.WriteLine();
            Console.WriteLine(MyMath.PI);



        }
    }
}
