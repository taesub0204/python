#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함
#include <string.h>

/*
* Num_arr[] 배열 > 포인터 함수
Num_arr = [6,8,2,9,4,7]를 선택정렬 알고리즘을 이용하여 오름차순으로 정렬하시오.
*/
void SelectionSort(int* Num_arr, int length);


int main(void)
{
	int Num_arr[] = { 6, 8, 2, 9, 4, 7 };
	int length = sizeof(Num_arr) / sizeof(int); // 전역변수
	//int length = 6; // 전역변수
	printf("Original data:");
	for (int i = 0; i < length; i++) printf("%d", Num_arr[i]);
	printf("\n================================2===========================\n");
	SelectionSort(Num_arr, length); // 함수 호출
	printf("\n\n");
	return 0;




}


void SelectionSort(int* Num_arr, int length)
{

	
		int i,j, temp, min_index;

		for (int i = 0; i < length-1; i++)
		{
			/*min_index = i*/

			for (int j = i + 1; j < length; j++)
			{
				if (Num_arr[i] > Num_arr[j]) {
					temp = Num_arr[i];
					Num_arr[i] = Num_arr[j];
					Num_arr[j] = temp;
				}
			}
		}

		for (int i = 0; i < length; i++)
		{
			printf("%d", Num_arr[i]);
		}
		printf("\n");
 






}


