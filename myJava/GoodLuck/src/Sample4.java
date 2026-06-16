import java.util.Scanner;

public class Sample4 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		Scanner s = new Scanner(System.in);
		int kor = s.nextInt();
		int math = s.nextInt();
		int eng = s.nextInt();
		
		double avg = (kor+math+eng)/3.0;
		System.out.printf("평균 = %.2f\n", avg);
		
		if(avg >= 90)
			System.out.println("A학점");
		else if(avg >= 80)
			System.out.println("B학점");
		else if(avg >= 70)
			System.out.println("C학점");
		else if(avg >= 60)
			System.out.println("D학점");
		else 
			System.out.println("F학점");
		
		
int scoreKey = (int)avg / 10;
System.out.println(scoreKey);
		
		switch (scoreKey) {
			case 10: // 100점인 경우 아래 case 9와 같이 처리됨 (break가 없으므로)
			case 9:
				System.out.println("A학점");
				break;
			case 8:
				System.out.println("B학점");
				break;
			case 7:
				System.out.println("C학점");
				break;
			case 6:
				System.out.println("D학점");
				break;
			default: // 60점 미만 모든 경우
				System.out.println("F학점");
		}

		
		
//		switch (scoreKey) {
//	    case 10, 9 -> System.out.println("A학점");
//	    case 8     -> System.out.println("B학점");
//	    case 7     -> System.out.println("C학점");
//	    case 6     -> System.out.println("D학점");
//	    default    -> System.out.println("F학점");
		

		
		
		
		

	}

}
