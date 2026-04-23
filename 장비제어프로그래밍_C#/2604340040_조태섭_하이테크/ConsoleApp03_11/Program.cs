using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp03_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 변수를 선언합니다.
            Console.WriteLine("숫자를 입력하세요 : ");
            int input = int.Parse(Console.ReadLine());

            switch (input % 2)
            {
                case 0:
                    Console.WriteLine("짝수입니다.");
                    break;
                case 1:
                    Console.WriteLine("홀수입니다.");
                    break;

                    // 메뉴만들 때 1, 2 3, 메뉴를 받아서 안에서 진행되게끔 할 때도 switch문을 많이 사용합니다.


            }
            Console.WriteLine("몇 월인지 월을 입력하세요 : ");
            int mon = int.Parse(Console.ReadLine());
            switch (mon)
            {
                case 12:
                case 1:
                case 2:
                    Console.WriteLine("겨울 입니다.");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("봄 입니다.");
                    break;
                case 6:
                case 7:
                case 8:
                    Console.WriteLine("여름 입니다.");
                    break;  
                case 9:
                case 10:
                case 11:
                    Console.WriteLine("가을입니다.");
                    break;
                default:
                    Console.WriteLine("대체 어떤 행성에 살고 계신가요?");
                    break;





            }


            /*
             * 
             * 
             * 
             * 
           영수증
           주문내역 
                 
            1. 아메리카노 2000 
            2. 라떼 3000
            3. 녹차 2500

            주문받기
            1. 잔수, 금액
            2. 잔수, 금액
            3. 잔수, 금액
            
            전체금액이 만원 이상이면 전체 금액 15% 해주시고
            2만원 이상이면 전체 금액 20% 해주세요.
             
            지금 까지 배운거 활용 해서 영수증 출력 
            ex)

           영수증
            
            주문내역

            항목/ 잔수/ 금액


            1. 아메리카노 3잔  6000원
            2. 라떼 2잔  6000원
            3. 녹차 1잔  2500원
               합계     6잔  14500원 (할인적용)
               할인금액 2175원
                지불금액 14500 - 2175 = 12325원 (최종금액)


           
             
             */


        }
    }
}
