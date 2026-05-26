#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
5명의 학생의 이름과 점수를 배열에 입력 받아서, 학생들의 이름과 석차를 출력하시오.

*/




int main()
{


	char name[5][5];
	int score[5][2];


	for (int i = 0; i < 5; i++)
	{
		score[i][1] = 1;
		printf("\n이름 : ");
		scanf("%s", name[i]);
		printf("\n점수 : ");
		scanf("%d", &score[i][0]);
	}

	for (int i = 0; i < 5; i++)
	{
		for (int j = 0; j < 5; j++)
			while (score[i][0] < score[j][0])
			{
				score[i][1]++;
				break;
			}
	}
	for (int i = 1; i < 6 ; i++)
		for (int j = 1; j < 6;j++)
			while (score[j][1] == i)
			{
				printf("");

			}



}