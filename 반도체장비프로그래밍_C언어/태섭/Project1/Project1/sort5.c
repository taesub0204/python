#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*

이름과 과목 A,B 점수를 입력 받아서 총점이 높은 순서대로 sort
출력하는 포르그램을 작성하세요.


*/


int main(void)
{

	char name[5][20];
	int scoreA[5]; // A점수
	int scoreB[5];// B점수
	int total[5];
	int i;
	int j;

	for (i = 0; i < 5; i++)
	{
	
		printf("\n이름 : ");
		scanf("%s", name[i]);
		printf("\n점수 : () () ");
		scanf("%d %d", &scoreA[i], &scoreB[i]);
		total[i] = scoreA[i] + scoreB[i];
		
		
	}

	int max_idx;

	for (i = 0; i < 4; i++) {
		max_idx = i; // 현재 위치를 가장 큰 점수의 위치라고 가정
		for (j = i + 1; j < 5; j++) {
			if (total[j] > total[max_idx]) {
				max_idx = j; // 더 큰 점수를 발견하면 위치를 기억
			}
		}

		 
		// [중요] 큰 값을 찾았으면 실제로 자리(Swap)를 바꿔주는 코드가 필요합니다!
		// max_idx  10 20 30 40,  i 1 2 3 4
		if (max_idx != i) {
			// 총점 교환
			int tempT = total[i]; 
			total[i] = total[max_idx]; 
			total[max_idx] = tempT;


			// A점수 교환
			int tempA = scoreA[i]; 
			scoreA[i] = scoreA[max_idx]; 
			scoreA[max_idx] = tempA;

			// B점수 교환
			int tempB = scoreB[i]; 
			scoreB[i] = scoreB[max_idx]; 
			scoreB[max_idx] = tempB;

			// 이름 교환
			char tempN[20];
			strcpy(tempN, name[i]); 
			strcpy(name[i], name[max_idx]); 
			strcpy(name[max_idx], tempN);
		}




	}

	printf("\n--- 정렬 결과 ---\n");
	for (i = 0; i < 5; i++) {
		// %d를 추가하고 scoreA[i], scoreB[i]를 인자로 전달합니다.
		printf("%d등: %s (총점: %d, A: %d, B: %d)\n", i + 1, name[i], total[i], scoreA[i], scoreB[i]);
	}

}
