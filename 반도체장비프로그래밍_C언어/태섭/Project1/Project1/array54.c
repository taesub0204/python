#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

배열 num에 {3,2,4,2,3,2,9,5,7}
임의의 수보다 큰 수의 개수를 구하는 코드를 작성하시오.

임의의 수 : 입력 받는 변수
5가 입력되면 5보다 큰수가 몇개냐??






*/




int main()
{
	int number;
	int num[] = { 3,2,4,2,3,2,9,5,7 };
	int i;
	int count=0;
	scanf("%d",&number);
	
	for (int i = 0; i < 9; i++)
	{
		if (num[i] > number) 
		{
			count++;
			

		}

	}

	printf("%d", count);


}