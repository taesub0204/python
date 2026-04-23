using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace coffee_order_for
{
    internal class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("1. 아메리카노  2000원"); 
            Console.WriteLine("2. 카페라떼    3000원");
            Console.WriteLine("3. 녹차        2500원");
            Console.WriteLine("99 . 주문종료");



            //int input_order_menu;
            //int input_order_count;
            int total = 0; // 총 주문 금액 변수
            int count1 = 0; // 아메리카노 누적 잔수
            int count2 = 0; // 카페라떼 누적 잔수
            int count3 = 0; // 녹차 누적 잔수

            while (true)
            {
                Console.WriteLine("주문받기 : ");
                //  주문 입력 받기 
                int input_order_menu = int.Parse(Console.ReadLine());


                if (input_order_menu == 99)
                {
                    Console.WriteLine("주문이 종료되었습니다.");
                    Console.WriteLine("\n===== 주문내역 =====");
                    Console.WriteLine("항목\t\t잔수\t단가\t합계");
                    if (count1 > 0) Console.WriteLine($"아메리카노\t{count1}\t2000\t{count1 * 2000}");
                    if (count2 > 0) Console.WriteLine($"카페라떼\t{count2}\t3000\t{count2 * 3000}");
                    if (count3 > 0) Console.WriteLine($"녹차\t\t{count3}\t2500\t{count3 * 2500}");
                    Console.WriteLine("총 주문 금액: " + total + "원");


                    // [거스름돈 로직 시작]
                    while (true) // 돈이 부족할 경우를 대비해 반복문 사용
                    {
                        Console.Write("지불할 금액을 입력하세요: ");
                        int inputMoney = int.Parse(Console.ReadLine());

                        if (inputMoney >= total)
                        {
                            int change = inputMoney - total; // 거스름돈 계산
                            Console.WriteLine($"\n결제가 완료되었습니다.");
                            Console.WriteLine($"지불금액: {inputMoney}원");
                            Console.WriteLine($"거스름돈: {change}원");
                            Console.WriteLine("이용해주셔서 감사합니다!");
                            break; // 결제 완료 시 반복문 탈출
                        }
                        else
                        {
                            // 돈이 부족한 경우
                            Console.WriteLine($"금액이 {total - inputMoney}원 부족합니다. 다시 입력해주세요.");
                        }


                        
                    }
                    break;
                }


               int price = 0; // 가격 변수
               int menu_total = 0; // 총 가격 변수

                if (input_order_menu == 1)


                {
                    Console.WriteLine("아메리카노 주문");
                    price = 2000; // 아메리카노 가격 설정
                }
                else if (input_order_menu == 2)
                {
                    Console.WriteLine("카페라떼 주문");
                    price = 3000; // 카페라떼 가격 설정
                }
                else if (input_order_menu == 3)
                {
                    Console.WriteLine("녹차 주문");
                    price = 2500; // 녹차 가격 설정
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 혹은 주문을 먼저하세요");
                    continue; // 잘못된 입력 시 다시 주문 받기
                }

          
                Console.WriteLine("잔수 입력 : ");
                 //  잔수 입력 받기 
                int input_order_count = int.Parse(Console.ReadLine());


                // 2. 입력받은 잔수를 각 변수에 누적
                if (input_order_menu == 1) count1 += input_order_count;
                else if (input_order_menu == 2) count2 += input_order_count;
                else if (input_order_menu == 3) count3 += input_order_count;





                menu_total = price * input_order_count; // int 제거 




                Console.WriteLine("총 가격 : " + menu_total + "원");

                total += menu_total; // 💥 핵심


            }







            }
    }
}
