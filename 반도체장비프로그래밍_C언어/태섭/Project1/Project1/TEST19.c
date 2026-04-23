#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

정수를 입력 받아서 "양수", "음수", "0"을 출력하는 순서도를 작성하시오.

*/

int main()
{
	int a;

	scanf("%d", &a);
	if (a > 0) printf("%d 는 양수", a);
	else if (a < 0) printf("%d 는 음수", a); // else 다음에 어떤 명령어 와도 상관없음
	else printf("%d는 0", a);



}
