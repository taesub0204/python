#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*



*/

int main()
{


	int n;
	label:
	printf("정수를 입력하세요");
	scanf("%d",&n);

	if (n == 0) goto end; // end로 가라
	if (n % 2)
		printf("홀수입니다. ");
	else
		printf("짝수입니다. ");
	goto label;     // 무한 반복 가능 해당 라벨로 가라
	end:;

	// goto 프로 일 수 록  if goto 가급적이면 자제


}



