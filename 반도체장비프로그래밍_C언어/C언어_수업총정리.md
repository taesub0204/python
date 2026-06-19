# C언어 수업 내용 총정리 및 리뷰

이 문서는 C언어 기초 수업에서 작성된 소스 코드들을 목차별로 분류하고, 주석 보충, 코드 리뷰 및 가상의 예상 실행 결과를 포함하여 정리한 자료입니다.

---

## 1장. 기본 문법 및 연산자, 조건문 (Basic & Conditionals)

이 장에서는 변수 선언, 기본 입출력(`printf`, `scanf`), 산술 연산자, 그리고 조건문(`if`, `switch`)을 다루는 코드들을 살펴봅니다.

### 1.1 변수 증감 연산자 (`TEST5.c`)
이 코드는 전위/후위 증감 연산자의 차이를 보여줍니다.
```c
#include<stdio.h>

int main()
{
	int a = 5, b = 3;
	a++; // a는 6이 됨
    
	// --a는 전위 감소이므로 6에서 5가 된 후 출력됨.
	// b++는 후위 증가이므로 3이 출력된 후 4가 됨.
	printf("a=%d b=%d\n", --a, b++);
    
	// 앞선 b++에 의해 b는 4가 된 상태
	printf("a=%d b=%d", a, b);

    return 0; // [보충] C언어 표준에서는 main 함수 끝에 return 0; 을 권장합니다.
}
```
**예상 실행 결과:**
```
a=5 b=3
a=5 b=4
```

### 1.2 화폐 매수 계산기 (`TEST6.c`, `TEST7.c`)
금액을 입력하면(여기서는 `127670` 고정), 각 화폐 단위별로 몇 개가 필요한지 계산하는 프로그램입니다.

```c
#include <stdio.h>

int main() {
    int money = 127670; // 계산할 금액
    // 화폐 단위 배열 선언 및 초기화
    int units[] = { 50000, 10000, 5000, 1000, 500, 100, 50, 10 }; 
    int count = 0; // 개수 저장 변수
    int total_count = 0; // 총 개수 누적 변수

    printf("금액: %d원\n", money);
    printf("--------------------------\n");

    for (int i = 0; i < 8; i++) {
        count = money / units[i];      // 몫: 해당 화폐로 거슬러 줄 수 있는 개수
        money = money % units[i];      // 나머지: 남은 금액
        total_count += count;          // 총 개수 누적

        if (count > 0) {
            printf("%d원: %d개\n", units[i], count);
        }
    }

    printf("--------------------------\n");
    printf("총 화폐 개수: %d개\n", total_count);

    return 0;
}
```
**예상 실행 결과:**
```
금액: 127670원
--------------------------
50000원: 2개
10000원: 2개
5000원: 1개
1000원: 2개
500원: 1개
100원: 1개
50원: 1개
10원: 2개
--------------------------
총 화폐 개수: 12개
```

### 1.3 사칙연산 (`TEST8.c`)
사용자로부터 두 정수를 입력받아 곱, 합, 차를 출력합니다.
```c
#define _CRT_SECURE_NO_WARNINGS // scanf 사용 시 보안 경고 무시
#include <stdio.h>

int main() {
	int a;  // 변수 a 선언
	int b;  // 변수 b 선언
	printf("두 정수를 공백으로 구분하여 입력하세요.\n");
    
	// 사용자로부터 2개의 정수를 입력받음
	scanf("%d %d", &a, &b); 

	int c1 = a * b; // 곱
	int c2 = a + b; // 합
	int c3 = a - b; // 차

	printf("곱: %d\n 합:%d \n 차:%d", c1, c2, c3);

    return 0;
}
```
**예상 실행 결과 (입력: `10 5`):**
```
두 정수를 공백으로 구분하여 입력하세요.
10 5
곱: 50
 합:15 
 차:5
```

### 1.4 초 단위 시간을 시/분/초로 변환 (`TEST9.c`)
초(sec)를 입력받아 시간, 분, 초 단위로 변환합니다.
```c
#define _CRT_SECURE_NO_WARNINGS 
#include <stdio.h>

int main() {
	int sec;  // 초단위 값을 받을 변수
	int time, min; 
	printf("초 단위로 입력하세요.\n");
	scanf("%d", &sec); 

	time = sec / 3600;      // 3600초(1시간)로 나누어 '시' 계산
	min = (sec % 3600)/ 60; // 1시간 미만의 남은 초를 60으로 나누어 '분' 계산
	sec = (sec % 3600) % 60; // [이상한 부분 확인 필요] 원본 코드는 min % 60 이었으나, 남은 초를 계산하려면 sec % 60 이 맞습니다.
                            // 원래 코드: sec = min % 60; -> 잘못된 논리입니다.
	printf("%d시간 %d분 %d초", time, min, sec);

    return 0;
}
```
> [!WARNING]
> 원본 코드에서 `sec = min % 60;` 부분은 남은 초를 구하는 수식으로 적절하지 않습니다. 원본 입력된 총 초(sec)에서 남은 나머지를 구하려면 `sec = sec % 60;` 이 올바릅니다.

### 1.5 삼항 연산자를 이용한 조건문 (`TEST10.c`, `TEST11.c`)
삼항 연산자 `(조건) ? 참일_때 : 거짓일_때` 의 활용법입니다.
```c
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int main() {
	int age;
	int diff; 
	printf("나이를 입력하시오: ");
	scanf(" %d", &age);
    
	// 18세 미만이면 0(미성년), 그 이상이면 1(성인)
	diff = age < 18 ? 0 : 1; 
	printf("결과: %d  / 0이면 미성년, 1이면 성인\n", diff);

    return 0;
}
```
**예상 실행 결과 (입력: `15`):**
```
나이를 입력하시오: 15
결과: 0  / 0이면 미성년, 1이면 성인
```

### 1.6 If문 기초 (`TEST16.c`, `TEST19.c`)
양수, 음수, 0을 판별하는 if-else 구조입니다. (`TEST19.c` 기준)
```c
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int main()
{
	int a;
    printf("정수 입력: ");
	scanf("%d", &a);
    
	if (a > 0) {
        printf("%d는 양수\n", a);
    } else if (a < 0) {
        printf("%d는 음수\n", a);
    } else {
        printf("%d는 0\n", a);
    }

    return 0;
}
```
**예상 실행 결과 (입력: `-5`):**
```
정수 입력: -5
-5는 음수
```

### 1.7 스위치문 (switch-case) 응용 (`switch42.c`)
나이를 연령대별로 분류하는 도서관 프로그램입니다.
```c
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int main()
{
	int age = 1, z = 0, t = 0, f = 0, s = 0;

	printf("나이를 입력하세요 (0 입력 시 종료)\n");

	for (; age != 0 ;) // 0이 입력될 때까지 무한 반복
	{
		scanf("%d", &age);
        if(age == 0) break; // [보충] 0을 입력받으면 프로그램 종료가 필요합니다.

		switch (age / 10) // 10으로 나눈 몫으로 연령대(10대, 20대 등) 판별
		{
		case 0:
		case 1: 
		case 2:
			z++; // 0~29세 누적
			break;
		case 3:
		case 4:
			t++; // 30~49세 누적
			break;
		case 5:
			f++; // [이상한 부분 확인 필요] 원본 코드는 break; 뒤에 f++;가 있어 f++가 절대 실행되지 않는 데드코드(Dead Code) 상태입니다.
                 // 올바른 수정: f++; break; 순서로 변경해야 합니다.
			break;
		default:
			s++; // 60세 이상 누적
            break;
		}
		printf("------------------- \n");
		printf("0~29세 %d 명 \n", z);
		printf("30~49세 %d 명 \n", t);
		printf("50~59세 %d 명 \n", f);
		printf("60세 이상 %d 명 \n", s);
	}
    return 0;
}
```
> [!WARNING]
> 원본 코드 `case 5:` 에서 `break;`가 `f++;` 보다 먼저 작성되어 `f++` 연산이 무시되는 논리 오류가 있었습니다. 수정이 필요합니다.

### 1.8 무한루프와 매출 계산 (`coffee40.c`)
단일 커피 메뉴를 파는 커피숍의 하루 매출과 누적 고객 수를 구합니다.
```c
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int c = 1500; // 커피 가격 (1잔 1500원)
int ea;       // 주문 수량(명수)
int total_ea; // 주문 수량 누적
int m;        // 현재 결제 금액 (커피 단가 * 잔수)
int total = 0;// 매출 총액 누적

int main()
{
	for(; ;) // 무한 반복 (마감 전까지 계속 주문을 받음)
	{
		printf("커피 단일 메뉴 주문 받습니다. 몇명인가요? 가격은 1500원\n");
        printf("(종료하려면 0 입력): ");
		scanf("%d", &ea);
		
        if(ea == 0) break; // [보충] 무한루프를 탈출하기 위한 조건 추가

		m = c * ea; 
		total_ea += ea;
		printf("%d명 주문 받아서 %d 원입니다.. \n", ea, m);

		total += m; 

		printf("누적 매출: %d원 \n", total);
		printf("누적 명수: %d명 \n\n", total_ea);
	}
    return 0;
}
```
**예상 실행 결과 (입력: `3` 후 `0`):**
```
커피 단일 메뉴 주문 받습니다. 몇명인가요? 가격은 1500원
(종료하려면 0 입력): 3
3명 주문 받아서 4500 원입니다.. 
누적 매출: 4500원 
누적 명수: 3명 

커피 단일 메뉴 주문 받습니다. 몇명인가요? 가격은 1500원
(종료하려면 0 입력): 0
```
