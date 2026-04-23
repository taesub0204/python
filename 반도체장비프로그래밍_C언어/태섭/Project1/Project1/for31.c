#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

3의 배수 갯수

50 ~ 100 사이 의 3의 배수 갯수

*/
//int  i = 0; 전역변수
int main()
{
	int i;
	int cnt = 0;

	for (i = 51; i < 100; i++) //  안에 모두 생략 가능, 생략을 하면 조건을 참으로 봄....
	{
		if (i % 3 == 0)
		{
			cnt++; // 갯수를 셀 때, 판매 토탈 누적 
			printf("i = %d cnt = %d \n", i, cnt);
		}
	}


}



