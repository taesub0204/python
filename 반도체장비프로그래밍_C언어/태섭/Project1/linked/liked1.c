#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <malloc.h>
/*
데이터 검색
삽입 삭제
어떻게 할지

검색해서 10 , 7, 5
찾고자 하는 값을




*/
typedef struct Node {
    int data;
    struct Node* next; //Node구조체형을 가리키는 포인터변수 선언
}node;

typedef struct {
    node* point;
}head_point;

head_point* create_head(void);
// (위쪽 함수 선언부)
void node_Append(head_point* head);
void node_Print(head_point* head); //  요렇게 한 줄 적어두기!
void node_Search(head_point* head);
void node_Insert(head_point* head);
void node_Delete(head_point* head);

int num = 0;
int in_data = 1;

int main() {
    head_point* head;
    head = create_head();

    while (num != 6) {
        printf("\n1.노드 추가   2.데이터 검색   3.노드 삽입   4.노드 삭제   5.출력   6.종료.....번호를 선택하세요: ");
        scanf("%d", &num);
        printf("\n\n");
        switch (num) {
        case 1:
            node_Append(head);  break;
        case 2:
            node_Search(head);  break;
        case 3:
            node_Insert(head);  break;
        case 4:
            node_Delete(head);  break;
        case 5:
            node_Print(head);  break;
        case 6:
            printf("GOOD BYE.\n");  break;
        }
    }
}

head_point* create_head(void) {
    head_point* head;
    head = (head_point*)malloc(sizeof(head_point));
    head->point = NULL;
    return head;
}

void node_Append(head_point* head) {
    node* newNode;
    node* temp;

    printf("추가 데이터를 입력하세요(0:추가 종료): ");
    scanf("%d", &in_data);
    while (in_data != 0)

    {
        newNode = (node*)malloc(sizeof(node));
        newNode->data = in_data;
        newNode->next = NULL;

        if (head->point == NULL) {
            head->point = newNode;
        }
        else {
            temp = head->point;
            while (temp->next != NULL)
                temp = temp->next;
            temp->next = newNode;
        }
        printf("추가 데이터를 입력하세요(0:추가 종료): ");
        scanf_s("%d", &in_data);
    }
}





void node_Print(head_point* head)
{
    // 1. 임시 포인터 temp 선언하고 첫 번째 노드 주소(head->point) 대입하기
    node* temp = head->point;


    printf("현재 리스트: \n");
    // 3. temp가 NULL이 아닐 때까지 반복
    while (temp != NULL) {
        // 현재 노드의 데이터 출력
        printf("%p %d %p\n", temp, temp->data, temp->next);

        // temp를 다음 노드로 전진시키기
        temp = temp->next;
    }

    printf("NULL\n"); // 마지막을 알리는 표시
    return;
}


//==========================================================
// 데이터 검색 함수
//==========================================================

void node_Search(head_point* head)
{
    int searchData;

    // temp는 리스트를 이동하면서 확인할 임시 포인터
    node* temp = head->point;

    printf("찾을 데이터를 입력하세요 : ");
    scanf("%d", &searchData);

    // 몇 번째 노드인지 확인하기 위한 변수
    int count = 1;

    // temp가 NULL이 될 때까지 반복
    while (temp != NULL)
    {
        // 현재 노드의 데이터와 찾는 데이터가 같은지 비교
        if (temp->data == searchData)
        {
            printf("\n=========================\n");
            printf("데이터를 찾았습니다.\n");
            printf("%d번째 노드입니다.\n", count);
            printf("주소 : %p\n", temp);
            printf("데이터 : %d\n", temp->data);
            printf("=========================\n");

            // 찾았으면 함수 종료
            return;
        }

        // 다음 노드로 이동
        temp = temp->next;

        // 노드 번호 증가
        count++;
    }

    // 끝까지 찾았는데 없을 경우
    printf("찾는 데이터가 없습니다.\n");
}


//==========================================================
// 노드 삽입 함수
//==========================================================

void node_Insert(head_point* head)
{
    int findData;
    int insertData;

    // 리스트를 이동할 포인터
    node* temp = head->point;

    printf("어느 데이터 뒤에 삽입하시겠습니까? : ");
    scanf("%d", &findData);

    printf("삽입할 데이터를 입력하세요 : ");
    scanf("%d", &insertData);

    // 원하는 데이터를 찾을 때까지 반복
    while (temp != NULL)
    {
        if (temp->data == findData)
        {
            // 새로운 노드 생성
            node* newNode;

            newNode = (node*)malloc(sizeof(node));

            // 데이터 저장
            newNode->data = insertData;

            /*
                현재 상태

                temp ---> 다음노드

                새로운 노드를 temp 뒤에 연결해야 한다.
            */

            // 새로운 노드가 원래 다음 노드를 가리키도록 설정
            newNode->next = temp->next;

            // temp가 새로운 노드를 가리키도록 변경
            temp->next = newNode;

            printf("노드가 삽입되었습니다.\n");

            return;
        }

        // 다음 노드 이동
        temp = temp->next;
    }

    printf("삽입할 위치를 찾을 수 없습니다.\n");
}

//==========================================================
// 노드 삭제 함수
//==========================================================

void node_Delete(head_point* head)
{
    int deleteData;

    printf("삭제할 데이터를 입력하세요 : ");
    scanf("%d", &deleteData);

    // 현재 노드를 가리키는 포인터
    node* temp = head->point;

    // 이전 노드를 기억하는 포인터
    node* prev = NULL;

    // 리스트를 끝까지 탐색
    while (temp != NULL)
    {
        // 삭제할 데이터를 찾은 경우
        if (temp->data == deleteData)
        {
            /*
                첫 번째 노드를 삭제하는 경우

                head
                  │
                  ▼
                [10] -> [20] -> [30]

                삭제 후

                head
                  │
                  ▼
                [20] -> [30]
            */

            if (prev == NULL)
            {
                // head가 두 번째 노드를 가리키도록 변경
                head->point = temp->next;
            }
            else
            {
                /*
                    중간 또는 마지막 노드 삭제

                    prev          temp

                     ↓              ↓

                    10 -> 20 -> 30 -> 40

                    삭제 후

                    10 ----------> 30 -> 40
                */

                prev->next = temp->next;
            }

            // 메모리 반환
            free(temp);

            printf("노드가 삭제되었습니다.\n");

            return;
        }

        // 이전 노드를 현재 노드로 변경
        prev = temp;

        // 현재 노드를 다음 노드로 이동
        temp = temp->next;
    }

    printf("삭제할 데이터가 존재하지 않습니다.\n");
}