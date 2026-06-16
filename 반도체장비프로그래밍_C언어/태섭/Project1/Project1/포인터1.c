#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*
*/



void swap(int* a, int* b) {
	printf("a의 값 : %p b의 값 : %p\n", a, b);
	printf("a의 값 : %d b의 값 : %d\n", *a, *b);
	int temp = *a;
	*a = *b;
	*b = temp;
}





int main()
{
	int num1 = 10;
	int num2 = 20;

	printf("swap 전 : num1 = %d, num2 = %d\n", num1, num2);
	swap(&num1, &num2);
	printf("swap 후 : num1= %d, num2 = %d\n", num1, num2);



}


