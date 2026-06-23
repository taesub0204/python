import java.util.ArrayList;
import java.util.Scanner;

public class Sample5 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		// 10미만 자연수에서 3과 5 배수를 구하면 3, 5, 6, 9이다. 이들의 총합은 23이다.
		// 그렇다면 1000미만의 자연수에서 3과 5의 배수의 총합을 구하라.
		// 입력 받는 값은 1부터 999까지(1000 미만의 자연수)이다.
		// 출력하는 값은 3의 배수와 5의 배수의 총합이다.

		
		Scanner sc = new Scanner(System.in);
		int n = sc.nextInt();
		System.out.print(n);
		int count = 0;
		while(true) {
			if(n == 0)break;
				
			n = n/10;
			count++;
			
			
			
			
		}
		System.out.println(":" + count + "자리 정수");
		
		
//		String s  = n+"";
//		System.out.println(s.length() + "자리");
		
		
		
		
		
		
		
		
		
		
		
		
		
		
//		int sum = 0;
//		ArrayList<Integer> list = new ArrayList<>();
//		
//		for (int i = 1 ; i < n; i++)
//		{
//			if (i % 3 == 0 || i % 5 == 0)
//			{
//			
//				list.add(i);
//				sum += i;
//			}
//
//		
//			
//		}
//		for(int i =0; i < list.size(); i++)
//		{	
//			if(i!= list.size()-1)
//			System.out.print(list.get(i) + "+");
//			else
//			System.out.print(list.get(i)+ "=");
//		}
//		System.out.println(sum);
//		
//		
		
		

	}

}
