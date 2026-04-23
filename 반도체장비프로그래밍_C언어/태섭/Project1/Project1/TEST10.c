#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*



*/

int main() {
	int a = 10, b = 5, diff;
	diff = a < b ? b - a : a - b;
	printf("두 수의 차는 :%d \n", diff);
	printf("두 수의 차는 :%d \n", a < b ? b - a : a - b);



}