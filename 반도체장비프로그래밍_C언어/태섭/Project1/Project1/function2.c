#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> //매크로
/*

1. 덧셈
2. 뺄셈
3. 곱셈
4. 나눗셈 
5. 종료 
번호를 선택하세요 

*/


int select;


menu()
{
	int sel;
	printf("\n 1. 덧셈");
	printf("\n 2. 뺄셈");
	printf("\n 3. 곰셈");
	printf("\n 4. 나눗셈");
	printf("\n 5. 종료");
	printf("\n메뉴를 선택해 주세요");
	scanf("%d", &sel);
	return sel;

}





int main() // 
{

	int select = menu();
	while (select != 5)
	{
		switch (select) //
		{
		case 1:
			printf("\n덧셈루틴\n");
			break;

		case 2:
			printf("\n뺄셈루틴\n");
			break;

		case 3:
			printf("\n곱셈루틴\n");
			break;

		case 4:
			printf("\n나눗셈루틴\n");
			break;

		}

		select = menu();

	}













}

