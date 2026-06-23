#pragma execution_character_set("utf-8")
#define _CRT_SECURE_NO_WARNINGS // �����͸� �Է� ���� �� ���ȿ� ������ ���� ��, �ش繮���� �ذ��ϱ� ���ؼ� define ����
#include <stdio.h> //��ũ��
/*

1. ����
2. ����
3. ����
4. ������ 
5. ���� 
��ȣ�� �����ϼ��� 

*/


int select;


menu()
{
	int sel;
	printf("\n 1. ����");
	printf("\n 2. ����");
	printf("\n 3. ����");
	printf("\n 4. ������");
	printf("\n 5. ����");
	printf("\n�޴��� ������ �ּ���");
	scanf("%d", &sel);
	return sel;

}








int main() // 
{


	int select = menu();
	while (select != 5)
	{
		switch (select) //
		{
		case 1:
			add();
			printf("\n������ƾ\n");
			break;

		case 2:
			minus();
			printf("\n������ƾ\n");
			break;

		case 3:
			multi();
			printf("\n������ƾ\n");
			break;

		case 4:
			share();
			printf("\n��������ƾ\n");
			break;

		}

		select = menu();

	}













}

