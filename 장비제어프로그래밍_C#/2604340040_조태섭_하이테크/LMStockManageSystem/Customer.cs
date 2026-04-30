using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMStockManageSystem
{
    internal class Customer
    {
        // 고객ID
        public string customerID { get; set; }

        // 손님이름
        public string customerName { get; set; }

        // 성별 
        public string gender { get; set; }

        // 주민번호
        public string ssn { get; set; }

        // 주소
        public string address { get; set; }

        // 전화번호
        public string phoneNumber { get; set; }

        // 포인트
        public int points { get; set; }

        //계좌번호
        public string accountNumber { get; set; }


        // 손님 행위    (메서드)

        // 구매하기 메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void PurchaseProduct(string productName, int quantity)
        {
            Console.WriteLine($"{customerName}님이 {productName} {quantity}개를 구매하셨습니다.");
            // 구매 로직 추가 (예: 포인트 적립, 재고 감소 등)
            points += quantity * 10; // 예시로 구매한 수량에 따라 포인트 적립 (10포인트/개)
        }


        // 방문   메서드 (예시로 방문 날짜를 매개변수로 받음)
        public void VisitStore(DateTime visitDate)
       {
           Console.WriteLine($"{customerName} {customerID}님이 {visitDate.ToShortDateString()}에 매장을 방문하셨습니다.");
        }



        // 반품하기     메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void ReturnProduct(string productName, int quantity)
        {
            Console.WriteLine($"{customerName} {customerID}님이 {productName} {quantity}개를 반품하셨습니다.");
        }


        // 교환하기    메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void ExchangeProduct(string productName, int quantity)
        {
            Console.WriteLine($"{customerName} {customerID}님이 {productName} {quantity}개를 교환하셨습니다.");
        }

        //환불학 메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void RefundProduct(string productName, int quantity)
        {
            Console.WriteLine($"{customerName} {customerID}님이 {productName} {quantity}개를 환불하셨습니다.");
        }

        // 포인트 적립 메서드 (예시로 적립할 포인트를 매개변수로 받음)
        public void EarnPoints(int pointsToEarn)
        {
            points += pointsToEarn;
            Console.WriteLine($"{customerName} {customerID}님이 {pointsToEarn} 포인트를 적립하셨습니다. 현재 포인트: {points}");
        }

    }
}
