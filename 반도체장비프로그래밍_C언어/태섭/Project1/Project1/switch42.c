#define _CRT_SECURE_NO_WARNINGS // �����͸� �Է� ���� �� ���ȿ� ������ ���� ��, �ش繮���� �ذ��ϱ� ���ؼ� define ����
#include <stdio.h> // �Է°� ����� �޴� �Լ����� �ʿ��� �� �ڵ��Լ� ������ִ� ���ŷ���ϱ� <stino.h> �����߾� ������ ���� ����
/*

���� �������� �̿��ڸ� �Ʒ��� ���� ���ɴ뺰�� �����Ͻÿ�.

��>
0 ~ 29 �� : 0��
30 ~ 49�� : 0��
50 ~ 59�� : 0��
60�� : 0��



�̿����� �� �ο��� 00 �� �Դϴ�.  
�Էµ����ʹ� ���� �Դϴ�. 




*/






int main()
{
	int age=1, z = 0, t = 0, f = 0, s = 0;
	int people = 0;
	int count = 0;


	printf("���̸� �Է��ϼ���\n");

	for (;age!=0;) // �ݺ�
	{
	
		scanf("%d", &age);

		switch (age / 10) // �� ��� ����
		{
		case 0:
		case 1: 
		case 2:
			/*printf("0 ~ 29��\n");*/
			z++;
			break;
		case 3:
		case 4:
			/*printf("30 ~ 49��\n");*/
			t++;
			break;
		case 5:
			/*printf("50 ~ 59��\n");*/
			break;
			f++;
		default:
			s++;


		}
		printf("------------------- \n");
		printf("0~29 %d �� \n", z);
		printf("30~49 %d �� \n", t);
		printf("50~59 %d �� \n", f);
		printf("60~%d �� \n", s);


	}
	





}