#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>

// 학생 정보를 담을 구조체 정의
typedef struct student {
    char name[15]; // 이름
    int score[3];  // score[0]: 과목A, score[1]: 과목B, score[2]: 총점
} student; // 별명 정의

int main()
{
    struct student Student[5], temp;
    int index = 0;

    // 5명의 이름과 성적 입력 받기
    for (int i = 0; i < 5; i++)
    {
        scanf("%s %d %d", Student[i].name, &Student[i].score[0], &Student[i].score[1]);
        Student[i].score[2] = Student[i].score[0] + Student[i].score[1]; // 총점 계산
    }

    // 선택 정렬 (Selection Sort) 알고리즘
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
        // 구조체 통째로 스왑(Swap)
        temp = Student[index];
        Student[index] = Student[i];
        Student[i] = temp;
    }

    // 결과 출력 (0번 인덱스인 1등부터 출력되도록 i=0으로 수정)
    for (int i = 0; i < 5; i++)
    {
        printf("%s %d %d %3d\n", Student[i].name, Student[i].score[0], Student[i].score[1], Student[i].score[2]);
    }

    return 0;
}