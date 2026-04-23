#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
33. 1인 운영 커피숍이 있다. 커피숍의 하루 매출과 고객 수를 산출하시오. 마감 시점은 사장의 퇴근이다.
입력 팀당 인원 , 팀당 결제 비용
*/

int c = 1500; // 커피 가격
int ea; // 명수 = 잔수 
int total_ea; // 명수 누적
int m;       //  커피 * 잔수
int total = 0; // 영수금액 누적


int main()
{
	

	for(; ;) //마감 시점전 까지 주문 받아야 함..
	{


		printf("커피 단일 메뉴 주문 받습니다. 몇명인가요? 가격은 1500원\n");
		scanf("%d", &ea);
		
		m = c * ea; // 커피 * 잔수(명수)
		total_ea += ea;
		printf("%d명 주문 받아서 %d 원입니다.. \n", ea, m);

		total += m; // 누적 매출


		printf("%d 누적 매출 \n", total);
		printf("%d 명수 누적 \n", total_ea);
		

	}
}