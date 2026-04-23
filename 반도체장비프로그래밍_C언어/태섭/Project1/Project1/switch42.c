#define _CRT_SECURE_NO_WARNINGS // 데이터를 입력 받을 떄 보안에 문제가 있을 때, 해당문제를 해결하기 위해서 define 해줌
#include <stdio.h> // 입력과 출력을 받는 함수들이 필요한 데 코딩함수 만들어주는 번거러우니까 <stino.h> 저장했어 꺼내서 쓰는 거임
/*

공도 도서관의 이용자를 아래와 같이 연령대별로 구분하시오.

예>
0 ~ 29 세 : 0명
30 ~ 49세 : 0명
50 ~ 59세 : 0명
60세 : 0명



이용자의 총 인원은 00 명 입니다.  
입력데이터는 나이 입니다. 




*/






int main()
{
	int age=1, z = 0, t = 0, f = 0, s = 0;
	int people = 0;
	int count = 0;


	printf("나이를 입력하세요\n");

	for (;age!=0;) // 반복
	{
	
		scanf("%d", &age);

		switch (age / 10) // 몫만 사용 가능
		{
		case 0:
		case 1: 
		case 2:
			/*printf("0 ~ 29세\n");*/
			z++;
			break;
		case 3:
		case 4:
			/*printf("30 ~ 49세\n");*/
			t++;
			break;
		case 5:
			/*printf("50 ~ 59세\n");*/
			break;
			f++;
		default:
			s++;


		}
		printf("------------------- \n");
		printf("0~29 %d 명 \n", z);
		printf("30~49 %d 명 \n", t);
		printf("50~59 %d 명 \n", f);
		printf("60~%d 명 \n", s);


	}
	





}