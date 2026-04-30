using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp05_13
{
    class Product
    {
        public string name;
        public int price;
    }
    class MyMath
    {
        public static double PI = 3.141592; // static은 클래스에 붙는거야. 그래서 MyMath.PI로 접근할 수 있어. MyMath myMath = new MyMath(); 이렇게 인스턴스 만들 필요 없어.
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Product product = new Product() { name = "감자", price = 2000 }; // 인스턴트 하나만 만들었어 product
            Product product1 = new Product() { name= "고구마", price = 3000}; // 인스턴트 하나만 만들었어 product1
            Product product2 = new Product() { name = "옥수수", price = 4000 }; // 인스턴트 하나만 만들었어 product2

            //product.name = "감자";
            //product.price = 2000;

            //product1.name = "고구마";
            //product1.price = 3000;




            Console.WriteLine(product.name+" : " + product.price + "원"); // 배열이 아니여서 하나만 들어감...
            Console.WriteLine(product1.name + " : " + product1.price + "원");
            Console.WriteLine(product2.name + " : " + product2.price + "원");    

            product.price = 5000;
            Console.WriteLine(product.name + " : " + product.price + "원"); 

            Console.WriteLine(MyMath.PI);


        }
    }
}
