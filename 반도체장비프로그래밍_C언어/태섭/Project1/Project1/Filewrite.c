#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*
*
*내가 찾는 사람의 점수가 몇점이냐 ?
것을 찾을 수 있게
if(strcmp(Student[i].name, searchName) == 0)
*
*/



struct student {

	char name[15]; //문자열
	int score; //정수

};



int main()

{
	struct student Student[3];
	FILE* fp = fopen("student_data.txt", "r");


	printf("3명의 이름과 점수를 입력하세요\n");
	for (int i = 0; i < 3; i++)
	{
		fscanf(fp,"%s %d", Student[i].name, &Student[i].score);
		printf("%s %d\n", Student[i].name, Student[i].score);
	}



	fclose(fp);




}
