using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMStockManageSystem
{
    internal class Employee
    {
        // 사원번호 
        public string employeeID { get; set; }

        // 사원 이름
        public string employeeName { get; set; }

        // 주민번호
        public string ssn { get; set; }

        // 성별
        public string gender { get; set; }

        //주소
        public string address { get; set; }

        //전화번호
        public string phoneNumber { get; set; }

        // 직급
        public string position { get; set; }

        //생년월일
        public string birthDate { get; set; }

        //계좌번호
        public string accountNumber { get; set; }

        // 직무
        public string jobTitle { get; set; }

        //출퇴근여부
        public bool isWorking { get; set; } // true: 출근, false: 퇴근

        // 직원  행위 (메서드)
        // 출근 메서드 사원번호로 
        public void ClockIn()
        {
            isWorking = true;
            Console.WriteLine($"{employeeID}님이 출근하셨습니다.");
        }

        // 퇴근 메서드
        public void ClockOut()
        {
            isWorking = false;
            Console.WriteLine($"{employeeID}님이 퇴근하셨습니다.");
        }

        // 판매하기 메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void SellProduct(string productName, int quantity)
        {
            Console.WriteLine($"{employeeID}님이 {productName} {quantity}개를 판매하셨습니다.");
        }

        // 계산하기 메서드 (예시로 총 금액을 매개변수로 받음)
        public void CalculateTotal(int totalAmount)
        {
            Console.WriteLine($"{employeeID}님이 총 금액 {totalAmount}원을 계산하셨습니다.");
        }

        // 재고 관리 메서드 (입고  메서드와 출고 메서드로 나눌 수 있음)
        public void ManageStock(string productName, int quantity, bool isIncoming)
        {
            if (isIncoming)
            {
                Console.WriteLine($"{employeeID}님이 {productName} {quantity}개를 입고하셨습니다.");
            }
            else
            {
                Console.WriteLine($"{employeeID}님이 {productName} {quantity}개를 출고하셨습니다.");
            }
        }

        //청소하기 메서드
        public void CleanStore()
        {
            Console.WriteLine($"{employeeID}님이 매장을 청소하셨습니다.");
            Console.WriteLine($"{employeeID}\n{employeeName}님이 매장을 청소하셨습니다.");
        }

        // 상품폐기 메서드 (예시로 상품명과 수량을 매개변수로 받음)
        public void DisposeProduct(string productName, int quantity)
        {
            Console.WriteLine($"{employeeID}님이 {productName} {quantity}개를 폐기하셨습니다.");
        }


    }
}
