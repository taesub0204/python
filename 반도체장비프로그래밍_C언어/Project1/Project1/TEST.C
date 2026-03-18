#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int main()
{
	int age;
	printf("age?");
	scanf("%d", &age);

	printf("age: %d", *(&age));
}

/*SCAN에서만 & &는 주소, 포인터 변수&age 저 주소에다가 데이터를 넣어라 바로 H/W 영향이 있음 

메모리에는 디렉터리 개념 
윈도우에서는 폴더 개념 

int age; age를 변수로 쓰겠다 변수를 정의함.

각자의 값이 다르기 떄문에 -로 나옴



%p 는 16진 수 0000001EA8F0FC84 주소임 
%d 는 10진 수로 표현 

*는 주소가 가지고 있는 값 
*(&age) &age주소로 가서 값(*)을 가지고와

변수를 한글로 만들면 x / 현장에서 망신당함



*/