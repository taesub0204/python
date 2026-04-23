#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
39-2 




500마리의 돼지를 방목하는 양돈장이 있다. 
오늘은 총 중량 5000kg을 출하하는 날이다. 
돼지의 무게는 한마리씩 통과하는 길목에 계근대를 설치하여 측정을 한다. 

출하 대상의 돼지는 60kg 에서 80kg 까지 이다. 
오늘 출하하는 돼지 마리수와 총 중량을 출력하시오. 


만일 출하 목표량이 미치지 못할 경우에는 출하가 가능한 돼지의 마리 수와 총중량을 출력하시오.


total_pig_count 




*/

int today_kg = 5000; //오늘 총 중량 가능한 5000kg 
int pig_minus_ea; // 계근대 통과 시 카운트
int total_pig_minus_ea; // 전체 돼지 누적

int pig_minus_kg; // 계근대에 통과한 kg 
int pig_ea_kg; // 돼지 마리 * 무게 
int sum_pig_minus_kg; // sum으로 누적 



int main()
{
	for (;;) // 반복
	{
		printf("돼지 kg 입력하세요\n");
		scanf("%d", &pig_minus_kg);

		if (pig_minus_kg >= 60 && pig_minus_kg <= 80)
		{
			printf("통과");
			total_pig_minus_ea ++; // 통과된 돼지 카운트 ea
			today_kg -= pig_minus_kg; // 총 중량 5000 - 누적으로 계산
			sum_pig_minus_kg += pig_minus_kg; // 중량 누적
			printf("오늘 가능한 량  %d \n 돼지 마리 수 %d \n 통과된 돼지 누적 중량 %d Kg", today_kg, total_pig_minus_ea, sum_pig_minus_kg);
		}
		else
		{
			printf("중량이 맞지 않지 않습니다.");
		}
		if (today_kg <= 0)
		{
			printf("\n완료했습니다.");
			break;
		}
	}

	printf("고생하셨습니다.\n");
}