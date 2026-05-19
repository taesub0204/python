#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*




*/




int main()
{
	//int i;
	//scanf("%d", &i);
	//printf("%d\n",i);

	char name[16]; //  배열은 주소다. 그래서 주소값 16개  문자는 1차원 배열
	//scanf("%s", &name); 주소기 때문에 &가 필요 없음
	scanf("%s", name);// 따라서 &가 없어야 

	printf("내 이름은 \"%s\"입니다", name);
}