#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*




*/




int main()
{
	int arr[8]; // arr 배열의 주소 , 배열 변수는 주소값을 갖는다.
	int var = 10;//데이터가 들어감 
	int size = sizeof(arr); // 배열 sizeof arr = 32  
	int length = size / sizeof(int); // 32 나누기 4byte

	printf("arr = %p\n",arr); // 주소를 출력해라 16진수 주소
	printf("arr = %p\n", &arr[0]); // &를 넣어줘야 같은 주소 arr = 000000BD328FFBC8 4byte만큼 해서 아래와 같이됨
	printf("arr = %p\n", &arr[1]); // &를 넣어줘야 같은 주소 arr = 000000BD328FFBCC
	printf("arr = %p\n", &arr[2]);



	printf("arr = %p\n", arr[0]);// 다른 주소가 나옴 
	printf("var = %d\n", var);
	
	printf("배열의 크기:%d\r\n배열의 길이: %d", size, length);

}