#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*


배열 num에 { 3,6,4,2,8,4,9,1,7 }이 wjwkdehldj dlTek 
가장 큰 값을 찾을 수 있는 코드를 작성하세요

*/




int main()
{
	int num[9] = { 3,6,4,2,8,4,9,1,7 }; // 배열 초기화 했음
	int max = 0; // max값 비교 함 
	

	for(int i = 0 ; i < 9 ; i++) // i 0 ~ 8 까지 증가함 

		if (num[i] > max ) // num[i]는 순차적으로 맥스와 비교  초기 max는 0값 그래서 
		{
			max = num[i];    //  num[i] = 3 이 처음에 저장됨  그 다음에는 
			printf("%d\n", max);
			
		}

	printf("%d", max);




}