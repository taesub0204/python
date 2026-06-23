#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>

// 선택 정렬 함수 (이름 배열, 점수 배열, 배열 길이를 인자로 받음)
void SelectionSort(char(*name)[5], int(*num)[3], int length)
{
    int index = 0, temp[3];
    char temp_N[5];

    for (int i = 0; i < length - 1; i++)
    {
        index = i;
        for (int j = i + 1; j < length; j++)
        {
            if (num[index][2] < num[j][2]) // 총점 비교
            {
                index = j;
            }
        }

        // 1. 점수 배열 스왑 (과목1, 과목2, 총점 각각 대입)
        temp[0] = num[index][0];
        temp[1] = num[index][1];
        temp[2] = num[index][2];

        num[index][0] = num[i][0];
        num[index][1] = num[i][1];
        num[index][2] = num[i][2];

        num[i][0] = temp[0];
        num[i][1] = temp[1];
        num[i][2] = temp[2];

        // 2. 이름 문자열 스왑 (strcpy 사용)
        strcpy(temp_N, name[index]);
        strcpy(name[index], name[i]);
        strcpy(name[i], temp_N);
    }
}

int main()
{
    char name[5][5]; // 5명의 이름 (최대 4글자)
    int score[5][3]; // 5명의 성적 (과목1, 과목2, 총점)

    // 데이터 입력 받기
    for (int i = 0; i < 5; i++)
    {
        printf("\n이름 : ");
        scanf("%s", name[i]);

        printf("정수 점수2 : ");
        scanf("%d %d", &score[i][0], &score[i][1]);
        score[i][2] = score[i][0] + score[i][1]; // 총점 계산
    }

    // 정렬 함수 호출
    SelectionSort(name, score, 5);

    // 결과 출력
    printf("\n======= 결과 출력 =======\n");
    for (int i = 0; i < 5; i++)
    {
        printf("%s    %d %d %3d\n", name[i], score[i][0], score[i][1], score[i][2]);
    }

    return 0;
}