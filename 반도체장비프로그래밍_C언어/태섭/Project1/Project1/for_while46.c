#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
39-2




500마리의 돼지를 방목하는 양돈장이 있다.
오늘은 총 중량 5000kg을 출하하는 날이다.
돼지의 무게는 한마리씩 통과하는 길목에 계근대를 설치하여 측정을 한다.

출하 대상의 돼지는 60kg 에서 80kg 까지 이다.
오늘 출하하는 돼지 마리수와 총 중량을 출력하시오.


만일 출하 목표량이 미치지 못할 경우에는 출하가 가능한 돼지의 마리 수와 총 중량을 출력하시오.


total_pig_count

if문 없이 만들기


*/




main()
{
	int pig_kg = 1;
	int pig_pass_count=0;
	int today_pos_kg = 5000; // 오늘 출하가능한 중량 5000kg
	int sum = 0; // 더하기 위한 초기값


	while (sum < 5000 && pig_kg > 0)
	{
		printf("돼지 무게 입력\n");
		scanf("%d", &pig_kg);

		while (pig_kg >= 60 && pig_kg <= 80) // 반복
		{


			pig_pass_count++; // 마리
			sum += pig_kg; // 입력 받은 총 중량
			break;

		}
	}
	
	printf("정상 출하 %d개 중량 %d KG\n", pig_pass_count,sum);  
	printf("------------------------ \n");


}