#define _CRT_SECURE_NO_WARNINGS 
#include <stdio.h> 
#include <string.h>

// 구조체 정의 (typedef 제거하고 깔끔하게 선언)
struct student {
    char name[15];
    int score[3];  // score[0]: A과목, score[1]: B과목, score[2]: 총점
};

int main() // void main 대신 int main 사용
{
    struct student Student[5], temp;
    int index = 0;

    printf("5명의 이름과 과목 A, B 점수를 입력하세요:\n");
    for (int i = 0; i < 5; i++)
    {
        scanf("%s %d %d", Student[i].name, &Student[i].score[0], &Student[i].score[1]);
        // 입력받은 두 과목의 점수를 더해 총점(score[2])에 저장
        Student[i].score[2] = Student[i].score[0] + Student[i].score[1];
    }

    // 선택 정렬(Selection Sort) 알고리즘 - 총점 기준 내림차순
    for (int i = 0; i < 4; i++)
    {
        index = i;
        for (int j = i + 1; j < 5; j++)
        {
            if (Student[index].score[2] < Student[j].score[2])
            {
                index = j;
            }
        }
        // 구조체 변수끼리 통째로 교환 (값 전체가 복사됨)
        temp = Student[index];
        Student[index] = Student[i];
        Student[i] = temp;
    }

    printf("\n--- 총점 높은 순서대로 결과 출력 ---\n");
    // ?? i = 0부터 시작하도록 수정하여 1등부터 5등까지 모두 출력되게 함
    for (int i = 0; i < 5; i++)
    {
        printf("%s %d %d %3d\n", Student[i].name, Student[i].score[0], Student[i].score[1], Student[i].score[2]);
    }

    return 0;
}