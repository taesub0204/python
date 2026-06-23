#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*




*/

struct STag
{
	int m;
	char c;
};

int main(void)
{

	struct STag s;
	struct STag C;

	s.m = 3;
	C.m = 5;
	s.c = 'A';
	C.c = 'B';

	printf("s.m: %d s.C: %d", s.m, C.m);



}
