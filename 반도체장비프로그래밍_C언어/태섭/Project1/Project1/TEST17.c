#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

변수 NUM1 , NUM2에 두개의 정수를 입력 받아서 두 정수의 차를 출력하는 코드를 작성하시오.


*/

int main()
{
	int num1, num2, ans;

	scanf("%d %d", &num1, &num2);
	if (num1 > num2) ans = num1-num2;
	else ans = num2-num1;
	printf("%d", ans);
}
