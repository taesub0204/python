#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
	졍수 N을 입력 받아서 N단 구구단을 출력하는 순서도를 작성하시오.


	1. n을 scanf 로 입력
	2. for문으로 구구단 처리
		- 1 부터 9 까지 처리
		- 그 이후 * n 

	3. 출력 

*/

int main()
{
	
	int n;
	int i;
	int x; // 곱하기
	printf("N단을 입력해주세요");
	scanf("%d", &n);

	for (i = 1; i <= 9; i++)
	{
		/*printf("%d", i);*/
		x = n * i;
		printf("%d x %d = %2d\n", n, i , x);
	}




}



