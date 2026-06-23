#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*




*/
 
typedef struct STag
{
	int m;
	
}SType;

int main()
{

	struct STag s1;
	s1.m = 1;

	SType s2;
	s2.m = 2;

	printf("s1.m: %d, s2.m: %d", s1.m, s2.m);



}
