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
            printf("데이터검색.\n");  break;
        case 3:
            printf("노드 삽입.\n");  break;
        case 4:
            printf("노드 삭제.\n");  break;
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





        void node_Print(head_point * head)
        {
            // 1. 임시 포인터 temp 선언하고 첫 번째 노드 주소(head->point) 대입하기
            node* temp = head->point;


            printf("현재 리스트: \n");
            // 3. temp가 NULL이 아닐 때까지 반복
            while (temp != NULL) {
                // 현재 노드의 데이터 출력
                printf("%p %d %p\n",temp, temp->data, temp->next);

                // temp를 다음 노드로 전진시키기
                temp = temp->next;
            }
            
            printf("NULL\n"); // 마지막을 알리는 표시
            return;
        }
        


    


