#define _CRT_SECURE_NO_WARNINGS // µ•¿Ã≈Õ∏¶ ¿‘∑¬ πﬁ¿ª ãö ∫∏æ»ø° πÆ¡¶∞° ¿÷¿ª ∂ß, «ÿ¥ÁπÆ¡¶∏¶ «ÿ∞·«œ±‚ ¿ß«ÿº≠ define «ÿ¡‹
#include <stdio.h> //∏≈≈©∑Œ
/*

1. µ°º¿
2. ª¨º¿
3. ∞ˆº¿
4. ≥™¥∞º¿ 
5. ¡æ∑· 
π¯»£∏¶ º±≈√«œººø‰ 

*/


int select;


menu()
{
	int sel;
	printf("\n 1. µ°º¿");
	printf("\n 2. ª¨º¿");
	printf("\n 3. ∞ıº¿");
	printf("\n 4. ≥™¥∞º¿");
	printf("\n 5. ¡æ∑·");
	printf("\n∏ﬁ¥∫∏¶ º±≈√«ÿ ¡÷ººø‰");
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
			printf("\nµ°º¿∑Á∆æ\n");
			break;

		case 2:
			minus();
			printf("\nª¨º¿∑Á∆æ\n");
			break;

		case 3:
			multi();
			printf("\n∞ˆº¿∑Á∆æ\n");
			break;

		case 4:
			share();
			printf("\n≥™¥∞º¿∑Á∆æ\n");
			break;

		}

		select = menu();

	}













}

