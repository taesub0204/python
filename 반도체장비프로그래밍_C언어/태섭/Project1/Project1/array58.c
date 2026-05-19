#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
5명 학생의 이름과 점수를 배열에 입력 받아서, 1등 학생의 이름을 출력하세요.

*/




int main()
{


	char name[5];
	scanf("%s", name);   // name값 자체가 주소값을 가지고 있음  문자열
	printf("%s", name);



}