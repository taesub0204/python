#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

점수가 다음과 같이 저장되어 있다
score ={30,60,40,20,80,40,90,10,70}

각 점수별로 석차를 출력하시오.
1등, 2등, 3등

다중 반복문이 들어가야함
*/




int main()
{

	int score[] = {30, 60, 40, 20, 80, 40, 90, 10, 70};
	int rank = 1;



	for (int i = 0; i < 9;i++) 
	{
		rank = 1;

		for (int j = 0; j < 9; j++)
		{
			if(score[i] < score[j])
			rank++;
		}
		printf(" %d\n", rank);

	}






}