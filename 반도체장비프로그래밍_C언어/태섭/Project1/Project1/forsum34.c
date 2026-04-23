#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

5개의 정수 data를 입력 받아서 합을 구하는 코드를 작성하시오.

0. for로 scanf 입력 5회  
1. scanf로 입력 변수 n에 저장
2. sum에 변수 n 누적해서 더하기 
3. printf로 sum 출력 


*/

int main()
{
	int n; // n번 (입력 받기) 
	//int b;
	//int c;
	//int d;
	//int e;
	int input; // 입력 카운트
	int sum = 0;
	for (input = 1;input <= 5;++input) 
	{
	
		scanf("%d",&n); // 변수 입력 
		sum += n; // 입력된 변수 누적해서 더하기 
	}



	printf("5회 입력된 합 %d", sum); // 출력 sum 
}



