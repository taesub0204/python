#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

두 정수 m,n 을 입력 받아서  m~n 까지 합을 구하시오 
(for문 활용 , n > m)


ex 5 부터 10 

0. 변수 scanf m n 입력 받아야 함
1. m n을 입력 받아  for문 돌아 해당 길의 합  
2. 출력



*/

int main()
{
	int m; // n보다 작은 수가 들어가야 함 ex 0
	int n; // m  n > m 큰 수 
	int sum = 0; // sum 하기 위해서 변수 선언

	printf("m 값입력  (N>M) 해당조건으로");
	scanf("%d", &m);

	printf("n 값입력  (N>M) 해당조건으로");
	scanf("%d", &n);



	for (; m <= n; m++ ) // 생략 가능
	{
		sum += m; // 누적 sum 
		printf("%d ", sum);
		
	}


	printf("%d ", sum); // 더해진 sum을 출력하면 m ~n 만큼 더 해짐


}



