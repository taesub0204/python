#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함


int round;




void SelectionSort(int* num, int length); // 선언
void Print(int* arr, int length);// 추상화

int main(void)
{
	int Nums[] = { 8, 5, 7, 9, 3, 2 };
	int length = 6; // 전역변수
	printf("Original data:");
	for (int i = 0; i < length; i++) printf("%d", Nums[i]);
	printf("\n================================2===========================\n");
	SelectionSort(Nums, length); // 함수 호출
	printf("\n\n");
	return 0;




}


void SelectionSort(int* num, int length)
{

	int i, j, temp, min_index;


	for (i = 0; i < length - 1; i++) // i는 0 < 5  0,1,2,3,4  
	{
		min_index = i;
		for (j = i + 1; j < length; j++) // j = 1 ;  j < 6 ; j증가
		{
			if (num[min_index] > num[j])
			{
				min_index = j;
			}

		}
		temp = num[i];
		num[i] = num[min_index];
		num[min_index] = temp;
		Print(num, length);
	}








}
void Print(int* arr, int length)
{
	int round = 0;
	int i = 0;
	printf("\nRound %d", ++round);
	for (i = 0; i < length; i++)
	{
		printf("%2d", arr[i]);
	}

}