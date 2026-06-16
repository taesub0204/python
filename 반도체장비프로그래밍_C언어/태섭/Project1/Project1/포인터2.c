#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*
*/



void swap(int* a, int* b, int* c ) {
	int tmp;
	if (*a < *b)
	{
		tmp = *a;
		*a = *b;
		*b = tmp;
	}
	if (*b < *c)
	{
		tmp = *b;
		*b = *c;
		*c = tmp;
	
	}
	if (*a < *b)
	{
		tmp = *a;
		*a = *b;
		*b = tmp;

	}


}







int main()
{
	int num1, num2, num3;
	//int num1 = 100;
	//int num2 = 20;
	//int num3 = 30;
	printf("세 정수를 입력하세요 공백으로 구분");
	scanf("%d %d %d", &num1, &num2, &num3);


	swap(&num1, &num2, &num3);



	printf("크기 순: %d %d %d", num1, num2, num3);

}


