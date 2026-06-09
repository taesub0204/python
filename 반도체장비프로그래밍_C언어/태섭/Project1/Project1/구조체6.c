#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*

이름과 과목 A,B 점수를 입력 받아서 총점이 높은 순서대로 sort
출력하는 포르그램을 작성하세요.


*/


typedef struct student {

	char name[15]; //문자열
	int score[3];
};


void main()
{
	struct student Student[5], temp;
	int index = 0;
	for (int i = 0; i < 5; i++)
	{
		scanf("%s %d %d", Student[i].name, &Student[i].score[0], &Student[i].score[1]);
		Student[i].score[2] = Student[i].score[0] + Student[i].score[1];
	}
	for (int i = 0; i < 4; i++)
	{
		index = i;
		for (int j = i + 1; j < 5; j++)
			if (Student[index].score[2] < Student[j].score[2])
				index = j;
		temp = Student[index];
		Student[index] = Student[i];
		Student[i] = temp;



	}

	for (int i = 1; i < 5; i++)
		printf("%s %d %d %3d\n", Student[i].name, Student[i].score[0], Student[i].score[1], Student[i].score[2]);







}

