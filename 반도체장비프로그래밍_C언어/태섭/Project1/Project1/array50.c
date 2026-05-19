#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*
5개의 정수를 입력 받아서 배열에 저장하고 N번째 임력된 data 출력



*/




int main()
{

	int arr[5]; 


	for (int i = 0; i < 5; i++) // arr = &a[0] data
		scanf("%d", &arr[i]);// 배열값을 받아도 arr만 포인터임   입력 받을 때는 &가 꼭 필요함......

	for (int i = 0; i < 5; i++) // arr만 주소값  
		printf("%d", arr[i]); 




	//데이터 가 없는거 정보로 간주 

	// string 일때는  %s  null까지 앞에서 출력을 함.


}