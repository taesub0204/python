using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMStockManageSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Product product = new Product
            {
                // 우유 영어로
                productID = "P001",
                productName = "milk",
                price = 2500,
                stockQuantity = 100,
                exp = "2026년4월29일",
                category = "과자류"
            }; // Product 클래스의 인스턴스 생성
            // product 객체의 속성에 값 할당 (예시로 우유 상품 정보 입력)
            Console.WriteLine(product);
            Console.WriteLine();




            // 직원   Employee 클래스의 인스턴스 생성
            Employee employee = new Employee
            {
                employeeID = "E001",
                employeeName = "홍길동",
                ssn = "900101-1234567",
                gender = "남성",
                address = "서울시 강남구",
                phoneNumber = "010-1234-5678",
                position = "사원",
                birthDate = "1990년1월1일",
                accountNumber = "123-456-7890",
                jobTitle = "판매직"
            };
            // 직원 행위 
            //출근
            employee.ClockIn(); 

            //퇴근
            employee.ClockOut();

            // 판매하기
            employee.SellProduct("우유", 10); // 예시로 우유 10개 판매 (판매 처리)
            
            // 계산하기 (총금액)
            employee.CalculateTotal(25000); // 예시로 총 금액 2만 5천원 계산 (계산 처리)

            employee.ManageStock("우유", 50, true); // 예시로 우유 재고 50개 관리 (재고 관리 처리)


            employee.CleanStore(); // 매장 청소 (청소 처리)

            // 폐기하기 
            employee.DisposeProduct("우유", 20); // 예시로 우유 20개 폐기 (폐기 처리)






            // 고객   Customer 클래스의 인스턴스 생성
            Customer customer = new Customer
            {
                customerID = "C001",
                customerName = "김영희",
                gender = "여성",
                ssn = "920202-2345678",
                address = "서울시 서초구",
                phoneNumber = "010-9876-5432",
                points = 0,
                accountNumber = "987-654-3210"
             };

            Console.WriteLine(customer);
            // 사과 구매
            customer.PurchaseProduct("사과", 5); 
            // 우유 반품
            customer.ReturnProduct("우유", 2);
            Console.WriteLine();

            // 환불
            customer.ReturnProduct("사과", 5); // 예시로 사과 5개 반품 (환불 처리)
            
            //교환    
            customer.ExchangeProduct("사과", 3); // 예시로 사과 3개 교환 (교환 처리)

            //포인트 
            customer.EarnPoints(50); // 예시로 50포인트 적립 (포인트 적립 처리)








            //협력사 Partner 클래스의 인스턴스 생성
            Partner partner = new Partner
            {
                companyName = "ABC 공급업체",
                contactNumber = "02-123-4567",
                representative = "김대표",
                businessNumber = "123-45-67890",
                productName = "Milk",
                accountNumber = "111-222-3333",
                productQuantity = 1000
            };
            Console.WriteLine(partner);

            // 발주
            partner.ReceiveOrder("Milk", 500); // 예시로 우유 500개 발주
            // 납품
            partner.DeliverProduct("Milk", 500); // 예시로 우유 500개 납품

            // 결제 처리
            partner.ProcessPayment(1250000); // 예시로 125만원 결제 처리 (500개 * 2500원/개)

            // 생산하기
            partner.ProduceProduct("Milk", 1000); // 예시로 우유 1000개 생산













        }
    }
}
