#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> //매크로
/*
선택정렬
sort 알고리즘 
훈련 필요 자꾸해봐야함.

정렬을 많이 쓰는 데 
어떤 정렬을 쓰는 지도 생각을 해봐야됨.

선택, 삽입, 퀵, 버블, 머지 

머지는 시험에 출제 

실제로는 위 3가지

선택이나 버블은 구현이 쉽다.

*/


int select;


menu()
{
	int sel;
	printf("\n 1. 선택정렬");
	printf("\n 2. 버블정렬");
	printf("\n 3. 삽입정렬");
	printf("\n 4. 종료");
	printf("\n메뉴를 선택해 주세요");
	scanf("%d", &sel);
	return sel;

}








int main() // 
{


	int select = menu();
	while (select != 4)
	{
		switch (select) //
		{
\

		case 1:
			printf("\n선택정렬\n");
			
			//int Num_arr[6];
			int Num_arr[6] = { 6, 8, 2, 9, 4, 7 };
			int count = 0;

			for (int i = 0; i < 6; i++)
			{
				//printf("%d\n", Num_arr[i]);
				for (int j = 0; j < 6; j++)
				{
					if (Num_arr[i] < Num_arr[j])
					{
						count++;
						printf("\n%d \n%d",  Num_arr[i], count);
					}
				}

			}



			break;

		case 2:

			printf("\n버블정렬\n");
			break;

		case 3:

			printf("\n삽입정렬\n");
			break;

		}

		select = menu();

	}













}

