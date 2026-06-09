#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*




*/


int main(void)
{

	struct
	{
		int m, n;
		char c;
	}s;

	s.m = 3;
	s.n = 4;
	s.c = 'A';
	printf("s.m: %d %d %c" , s.m, s.n, s.c);





}
