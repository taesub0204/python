#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
5명의 학생의 이름과 점수를 배열에 입력 받아서, 학생들의 이름과 석차를 출력하시오.

*/




int main()
{

	int score[5] = {0};// 배열 선언 5개
	int rank = 1; // 순위를 구하기 위해서 1을 넣어줌
	


	char name[5][10]; // 5명의 이름(최대 9바이트)을 넣을 방 생성,   주소라고 함... 

	printf("5명의 이름과 숫자를 입력하세요 (엔터로 구분):\n");

	for (int i = 0; i < 5; i++) {

		scanf("%s", name[i]); // 배열이 주소기 때문에 &가 필요 없음
	}

	// 잘 들어갔나 출력 확인
	printf("\n--- 입력된 이름 ---\n");
	for (int i = 0; i < 5; i++) {
		printf("%s\n", name[i]);
	}



	printf("점수 입력하세요\n");

	for (int i = 0; i < 5; i++) {
		// 숫자를 입력받을 때는 변수 앞에 반드시 '&'를 붙여
		scanf("%d", &score[i]);
	}

	for (int i = 0; i < 5; i++) {
		printf("%d\n", score[i]); // 점수 출력 확인
	}


	for (int i = 0; i < sizeof(score) / sizeof(int);i++) // sizeof(score)는 4*5 = 20,    20 / 4   = 5만큼
	{
		rank = 1; //석차를 위한 알고리즘 상 1 추가

		for (int j = 0; j < sizeof(score) / sizeof(score[0]); j++)
		{
			if (score[i] < score[j]) // j 보다 작으면 순위를 카운트함
				rank++;
		}
		printf("이름 : %s, score=[%d]=%d 석차는 %d\n", name[i], i, score[i], rank);

	}


}