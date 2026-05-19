#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

사과 재배 농가에서 중량이 적절한 상품을 선별하고자 한다. 
표준 중량은 200g 이며 호용오차는 +_ 5g 이다.

선별 과정을 통과하지 못한 사과는 폐기한다. 
폐기할 사과의 비율(%)을 알려주는 코드를 작성하시오. 

조건) 선별기를 통과하는 사과의 개수는 모릅니다 
while 문을 사용






*/


// while(조건)만 있음  
// while (조건) = for(;조건;)


// do ~ while 실행을 먼저하고 조건 비교 


int apple; // 사과 중량 입력 
int apple_pass_count = 0;
int apple_fail_count = 0;
int apple_total_count;
int apple_fail_per;



main()
{
	while (1)
	{
	
	printf("사과무게 입력\n");  // 사과 입력
	scanf("%d", &apple); //  입력
	if (apple >= 195 && apple <= 205) // 195 ~ 205 범위라면
	{
		apple_pass_count++;
		printf("통과  %d개\n", apple_pass_count); // 통과
	}
	else
	{
		apple_fail_count++;
		printf("폐기 %d개\n", apple_fail_count); // 폐기
	}
	apple_total_count = apple_pass_count + apple_fail_count; // 통과 + 폐기 = 토탈사과
	printf("------------------------\n");
	printf("%d 토탈\n", apple_total_count); // 토탈사과 
	printf("------------------------\n");

	apple_fail_per = ((float)apple_fail_count / apple_total_count) * 100; // 폐기된 사과 / 토탈사과 * 100, 7/10 = 0.7 따라서 몫이 0 이 나오게됨 그래서 실수 float처리
	printf("폐기율 %d퍼센트 \n", apple_fail_per);
    }

}



