#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*
*/



typedef struct student {

	char name[15]; //문자열
	int score; //정수

};



void main()

{
	struct student Student;
	strcpy(Student.name, "홍길동");
	Student.score = 20;
	printf("이름: %s\n",Student.name);
	printf("나이: %d\n", Student.score);


}
