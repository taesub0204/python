#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> //매크로
/*
 두 정수를 입력 받아서 변수 large에는 큰 수를 small에는 작은 수를 저장하고 출력하는 코드를 작성 하세요

*/

int sum, sub; // 전역변수
int Sum(int a, int b); // 추상화 이름은 있는 데 어디 있는 지 모를 떄  


int main() // 함수
{
	Sum(1, 100); // 컴파일러가 함수 찾으러 감 봇다리(매개변수?) 가져가서 사용함.. 
	printf("sum: %d \nsub %d", sum, sub);

}




int Sum(int a, int b) // 함수는 리턴 값 한개, 여러개 받고 싶으면 배열로..
{ // 사용자가 만든 함수네 언제가 쓰것네
  // 지역변수
	
	sum = a + b; // 지역변수
	sub = b - a;
	return sum;
}


