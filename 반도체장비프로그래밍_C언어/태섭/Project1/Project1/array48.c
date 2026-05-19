#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*




*/




int main()
{
	//int arr[] = {1,1,1,1,1,1,1,1,1,1}; //
	//int alen = sizeof(arr) / sizeof(arr[0]); // 요소10개의 메모리(size)크기40 나누기 요소1개 크기4 = 10 

	//for (int i = 0; i < alen;i++)
	//	printf("arr[%d] = %d\n",i, arr[i]);



	char str[] = "C Programming for the first time";
	int slen = sizeof(str) / sizeof(str[0]) - 1; // 글자 하나씩 해서 사이즈를 잡음      32*4 / 4 = 32개 배열사이즈
	
	//printf("alen: %d\r\nslen: %d", alen, slen);
	printf("slen: %d\n", slen);
	/*for (int i = 0; i < slen;i++)*/
	printf("%s\n", str);
	for (int i = 0; i < slen;i++) // 32
		printf("str[%d] = %c\n",i, str[i]);

}