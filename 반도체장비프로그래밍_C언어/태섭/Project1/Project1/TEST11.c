#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

나이를 입력 받아서 미성년자를 찾아내시오. 
(18세 미만 미성년자)


나이 정수형 

입력 
속성:
출력


*/

int main() {
	int age;
	int diff; 
	printf("나이를 입력하시오");
	scanf(" %d", &age);
	diff = age < 18 ? 0 :1; // 활용하려면 변수 선언이 필요
	printf("결과: %d  / 0이면 미성년, 1이면 성인", diff);


	

}

//int main() {
//	int age;
//	scanf("%d", &age);
//	printf("%s", age < 18 ? "성인" : "미성년");
//
//
// a < b = 0 값 거짓 
// a > b = 1 값 참
//}