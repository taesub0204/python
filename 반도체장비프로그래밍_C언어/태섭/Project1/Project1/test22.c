#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

14. 4개 의 탁구공 중 무게각 다른 것 1개를 구하시오

*/

int main()
{
	int a, b, c, d; // a, b,c,d  탁구공  변수 선언
	scanf_s("%d %d %d %d", &a, &b, &c, &d); // scanf_s 해주면 보안 관련해서 ....

	if (a == b)
	{
		if (a == c)
		{
			printf("무게 다른 탁구공 %d", d);
		}
		else if (a == d)
		{
			printf("무게 다른 탁구공 %d", c);
		}
	}

	if (a == c) {
				if (a == d)
				printf("무게 다른 탁구공 %d", b);
				}
	if (b == c) {
				if (c == d)
				printf("무게가 다른 탁구공 %d", a);
				}
		

	//printf("%d\n",(2&2)); // 비트연산자 논리곱으로 연산
	//printf("%d\n", (2 && 2)); // 논리연산자
}
	
	
	
	
	
		



