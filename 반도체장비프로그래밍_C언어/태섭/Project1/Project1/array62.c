#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임 
/*
 두 정수를 입력 받아서 변수 large에는 큰 수를 small에는 작은 수를 저장하고 출력하는 코드를 작성 하세요

*/




int main()
{
	int large, small, temp;

	scanf("%d %d",&large, &small);

	if (large < small) // large가 작다면 참
	{
		temp = small; // temp에 small 넣음
		small = large; // large는 small보다 작았으니, small에 넣음
		large = temp; // temp는 다시 large에 넣음 정렬 알고리즘 중요
	}



			
	printf("large = %d \nsmall = %d", large, small);


}