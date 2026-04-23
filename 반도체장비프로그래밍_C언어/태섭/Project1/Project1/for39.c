#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
10명으로부터 기부금으 받습니다. 
총액과 평균(정수) 출력.






*/

int main()
{

	
	int i;
	int m; // 기부금 
	int sum = 0; // 합계 변수 선언
	int avg; // 평균 변수 선언
	printf("10명으로 부터 기부금 받습니다.\n");
	
	for (i = 1; i <= 10; i++) {
		printf("%d 번째 모금", i);
		scanf("%d",&m);

		sum += m;
		avg = sum / 10;
		
	}

	printf("총합 :%d, %f 절사%d \n", sum, sum/10., sum/10/100*100 );
	printf("평균 : %d %5.2f", avg, avg/10.); // 5.2f 해설 .. 전체 수가 5개,소수점 이하 2개 


}



