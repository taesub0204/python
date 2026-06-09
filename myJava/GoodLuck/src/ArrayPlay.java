import java.util.Scanner;



public class ArrayPlay {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Scanner sin = new Scanner(System.in);
		
		System.out.println("몇 개의 성적을 입력할거니?");
		int n = sin.nextInt();
		int[] score = new int[n];
		
		// int[] jumsu = {90, 50, 10, 60, 70}; // 5개의 공간이 생성, jumsu[0] ~ jumsu[4]
		int sum = 0;
		int max = 0;
		int min = 100;
		
		for (int i = 0 ; i < n ; i++)
		{
			score[i] = sin.nextInt();
			sum += score[i];
			if(max < score[i] )
				max = score[i];
			if(min >score[i])
				min = score[i];
			
		}

		
		double avg = sum/(double)n;
		
		System.out.println("=============================== 수학 과목 통계=====================================");
		for(int i = 0 ; i < n ; i++)
		{
			if(i < n-1)
			System.out.print(score[i]+",  ");
			else
			System.out.println(score[i]);
		}
		
		System.out.printf("** 총점 : %d\n", sum);
		System.out.printf("** 평균 : %.1f\n", avg);
		System.out.printf("** 최고점 : %d\n", max);
		System.out.printf("** 최저점 : %d\n", min);
		
		System.out.println("=================================================================================");
		
		

	}

}
