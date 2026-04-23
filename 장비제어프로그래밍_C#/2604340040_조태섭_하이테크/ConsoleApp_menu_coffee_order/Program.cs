using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApp_menu_coffee_order
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //int coffeeshop_menu;
            //int americano = 2000;
            //int latte = 3000;
            //int greentea = 3500;


            int americano = 2000;
            int latte = 3000;
            int greentea = 2500;



            Console.WriteLine("메뉴를 주문하세요 americano/latte/greentea ");
            string menu = Console.ReadLine();
            Console.WriteLine("갯수 주문하세요");
            int ea = int.Parse(Console.ReadLine());

      

                
















            //string age = Console.ReadLine();
            //string cellphone = Console.ReadLine();
            //string addr = Console.ReadLine();

            //uint sub1 = uint.Parse(Console.ReadLine());  // 형변환이 되지 않으면 연산이 되지 않음 
            ////int sub1_1 = 100;
            //int sub2 = int.Parse(Console.ReadLine());
            //int sub3 = int.Parse(Console.ReadLine());
            //int total = (int)sub1 + sub2 + sub3;


            Console.WriteLine("-------------------------------");
            Console.Write("menu : " + menu);
            Console.Write(" ea : " + ea);
  

            Console.WriteLine();

            switch (menu)
            {
                case "americano":
                    Console.WriteLine("아메리카노 " + ea + "잔 주문하셨습니다. 총 금액은 " + (americano * ea) + "원입니다.");
                    break;

                case "latte":
                    Console.WriteLine("라뗴 " + ea + "잔 주문하셨습니다. 총 금액은 " + (latte * ea) + "원입니다.");
                    break;

                case "greentea":
                    Console.WriteLine("라뗴 " + ea + "잔 주문하셨습니다. 총 금액은 " + (greentea * ea) + "원입니다.");
                    break;


            }
            Console.WriteLine("영수증");
            Console.WriteLine("*주문내역");
            Console.Write("*주문내역");







            //Console.Write("나이 : " + age);
            //Console.Write("전화번호 : " + cellphone);
            //Console.Write("주소 : " + addr);
            //Console.Write("과목 1 :" + sub1);
            //Console.Write("과목 2 :" + sub2);
            //Console.Write("과목 3 :" + sub3);
            //Console.Write("총점 : " + total);
            //Console.WriteLine("평균 : " + total / 3.0);
            //Console.WriteLine("-------------------------------");







        }
    }
}
