import java.util.Scanner;

public class PlayString {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
//		StringBuilder sb = new StringBuilder();
//		// Stringbuilder 굳이 import 하지 않아도 댐
//		// 아까는 java.utill.  써야 함, java.lang은 출력은 그냥 기본적으로 셋팅
//		sb.append("Hello");
//		sb.append(" ");
//		sb.append("Jump to Java!");
//		
//		System.out.println(sb);
//		
//		sb.insert(0,  "Good ");
//		
//		System.out.println(sb);
//		
//		sb.insert(11, "Fighting~~ ");
//		System.out.println(sb);
//		sb.delete(5, 11);
//		System.out.println(sb);
//		String s = sb.toString();
		
		Scanner s = new Scanner(System.in);
		int[] arr1 = new int[5]; // 방크기 0~9번까지 
		
		for(int i:arr1) //  방크기 0~9번까지 앞에서 부터 i 에게 줘
		{
			System.out.println(i+"\t");
		}
		for(int i = 0; i < arr1.length; i++) //  방크기 0~9번까지 앞에서 부터 i 에게 줘
		{
			arr1[i] =  s.nextInt();
		}
		
		for(int i : arr1)
			System.out.print(i + "\t");
		System.out.println();
		
		
		
		int sum = 0;
		double avg;
		
		for(int i : arr1)
			sum += i ;
		avg =(double) sum /arr1.length; //sum/10
		
		System.out.println("합 : " + sum + "평균 : "+avg+"");
		
		
		
		
				

	}

}
