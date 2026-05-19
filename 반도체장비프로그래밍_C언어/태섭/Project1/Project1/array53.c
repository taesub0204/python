#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*


배열 num에 {3,6,5,2,8,4,9,1,7}이 저장 되어있다.
찾는 값 임의의 수는 몇 번째 배열에 저장되어 있나요?
찾는 값이 없으면 "값이 없습니다. "를 출력하세요.

*/




int main()
{
	int num[] = { 3,6,5,2,8,4,9,1,7 };
	int i;
	int input; // 입력 받을 변수 
	int found = 0; // 찾았다면 1 없다면 0

    scanf("%d", &input); // 입력 받기
	for (i = 0; i < 9; i++)
	{


		if (num[i] == input) // 해당조건이 참이면 아래 출력문과 found = 1 
		{
			printf("숫자는 %d  배열은 %d번째 배열입니다.\n", input, i);
			found = 1; // 1과 0 으로 구분하게됨
			break;  
		}

	}

		if(found == 0)
		{
			printf("찾는 값이 없습니다. %d\n", input);

		}



	
	
}