#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*



*/




int main()
{
	char name[] = { "kkk" }; // 문자열을 초기값을 줄때는 [] 주면 자동으로 NULL이 들어감..   문자열에는 반드시 NULL이 있다.
	//name[2] = NULL;
	printf("%s", name); // name으로  /k/k/k/
						//           /0/1/2/ 스트링 뒤에는 NULL이 항상와야 함...



}