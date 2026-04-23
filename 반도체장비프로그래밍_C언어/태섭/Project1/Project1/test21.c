#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

14. 음료수를 병에 담는 공정이다. 3개의 제품 (a,b,c)를 비교했다. 이 중 한개는 함량이 부족하다. 정량이 아닌
제품을 찾아내시오. 기준 무게는 별도로 입력 받지 않는다

*/

int main()
{
	int a, b, c; // a, b,c 제품 변수 선언
	scanf("%d %d %d", &a, &b, &c);

	if (a == b) printf("%d 정량아닌", c);
	else if (a == c)  printf("%d 정량아닌", b);
	else printf("%d 정량아닌", a);




}