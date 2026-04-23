#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임


/*
	page 70P

*/

int main() {
	int a;  //변수 a선언 아무것도 없음
	int b; // 변수 b선언 아무것도 없음
	printf("두 정수를 공백으로 구분하여 입력하세요.\n");
	scanf("%d %d", &a, &b); // 정수를 넣어줘야 함 자료형 수동으로 지정 필요, 키보드를 통해서 데이터 받는 것을 scanf 그런데
	// 입력 받은 값 a , b 변수 갯수가 맞아야 함. 

	int c1 = a * b;// c1 값 넣어줌
	int c2 = a + b;// c2 값 넣어줌
	int c3 = a - b;// c3 값 넣어줌

	printf("곱: %d\n 합:%d \n 차:%d",c1,c2,c3);



}