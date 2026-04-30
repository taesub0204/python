using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMStockManageSystem
{
    internal class Partner
    {
        // 회사명
        public string companyName { get; set; }

        // 연락처
        public string contactNumber { get; set; }

        // 대표자
        public string representative { get; set; }

        // 사업자 번호
        public string businessNumber { get; set; }

        // 제품명(상품명)
        public string productName { get; set; }

        // 계좌번호
        public string accountNumber { get; set; }

        // 제품수량
        public int productQuantity { get; set; }


        // 협력사 행위     (메서드)

        // 발주받기  메서드 (예시로 제품명과 수량을 매개변수로 받음)

        public void ReceiveOrder(string productName, int quantity)
        {
            Console.WriteLine($"{companyName}에서 {productName} {quantity}개를 발주받으셨습니다.");
        }

        // 납품하기    메서드 (예시로 제품명과 수량을 매개변수로 받음)
        public void DeliverProduct(string productName, int quantity)
        {
            Console.WriteLine($"{companyName}에서 {productName} {quantity}개를 납품하셨습니다.");
        }
        // 수금 처리하기   메서드 (예시로 금액을 매개변수로 받음)
        public void ProcessPayment(int amount)
        {
            Console.WriteLine($"{companyName}에서 {amount}원을 수금 처리하셨습니다.");
        }


        // 상품 생산하기  메서드 (예시로 제품명과 수량을 매개변수로 받음)
        public void ProduceProduct(string productName, int quantity)
        {
            Console.WriteLine($"{companyName}에서 {productName} {quantity}개를 생산하셨습니다.");
        }


    }
}
