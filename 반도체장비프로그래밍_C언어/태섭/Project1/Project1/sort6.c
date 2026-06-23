#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*

이름과 과목 A,B 점수를 입력 받아서 총점이 높은 순서대로 sort
출력하는 포르그램을 작성하세요.


*/


int main(void)
{

	char name[5][5], temp_N[5];
	int score[5][3];

	int index = 0, temp[3] = { 0,0,0 };
	

	for (int i = 0; i < 5; i++)
	{
		score[i][1] = 1;
		printf("\n이름:");

		scanf("%s", &name[i]);
		printf("\nw점수1 점수2 : ");
		scanf("%d %d",)
	}


}
