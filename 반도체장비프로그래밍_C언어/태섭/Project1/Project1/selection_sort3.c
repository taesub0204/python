#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함


int round;
int Nums[] = { 6,8,2,9,4,7 };

int length = sizeof(Nums) / sizeof(int); // 전역변수

void SelectionSort(); // 선언
void Print();// 추상화

int main(void)
{

	printf("Original data:");
	for (int i = 0; i < length; i++) printf("%d", Nums[i]);
	printf("\n================================2===========================\n");
	SelectionSort(); // 함수 호출
	printf("\n\n");
	return 0;




}


void SelectionSort()
{

	int i, j, temp, min_index;


	for (i = 0; i < length - 1; i++) // i는 0 < 5  0,1,2,3,4  
	{
		min_index = i;
		for (j = i + 1; j < length; j++) // j = 1 ;  j < 6 ; j증가
		{
			if (Nums[min_index] > Nums[j])
			{
				min_index = j;
			}

		}
		temp = Nums[i];
		Nums[i] = Nums[min_index];
		Nums[min_index] = temp;
		Print();
	}








}
void Print()
{
	int i = 0;
	printf("\nRound %d", ++round);
	for (i = 0; i < length; i++)
	{
		printf("%2d", Nums[i]);
	}

}