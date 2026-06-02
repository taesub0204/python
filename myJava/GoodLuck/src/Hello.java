import java.util.Scanner;




public class Hello {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int a, b;
		
		
		Scanner s = new Scanner(System.in);
		System.out.println("입력해~~");
		// 표준 입력은 키보드 입력, 파일로부터 입력 받는 파일 입력, 표준출력은 화면 출력, 파일에다가 쓰는 파일 출력
		// 또 프린터로 출력하는 것도 있음
		// 여기서 System.in은 키보드입력
		// System.out은 화면 출력을 나타냄
		// Scanner 는 자바에서 제공하는 표준 라이브러리
		a = s.nextInt();
		//b = s.nextInt();
		
		
		
		//System.out.print(a+b);
		// 계층구조로 만들어진 언어
		
		String str = "Hello";
		String str1 = new String("hello");
		// 스캐너 객체 생성하는 거처럼 사용 가능
		// String 클래스니까
		System.out.println(str.charAt(2)); 
		System.out.println(str.substring(1));
		System.out.println(str.substring(1,4));
		System.out.println(str.toUpperCase());
		System.out.println(str.concat(str1));
		
		Integer a1;
		Float a2;
		Double a3;
		Character a4;
		
		/* 블록 주석*/

		if(str.equalsIgnoreCase(str1))
			System.out.println("같다");
		else
			System.out.println("다르다");
		
		int sum = 0;
		
		for(int i = 1; i <= a; i++)
			sum += i;
		System.out.println("1부터"+a+"까지의 합은"+sum+"입니다.");
		
		
		int i = 1;
		
		while(1 > a)
			{
			sum += i;
			i++;
		
				
			}
			System.out.println("1부터"+a+"까지의 합은"+sum+"입니다.");
			
			i =1;
			do {
				sum +=i;
				i++;
				
				}while(i<=a);
			
			System.out.println("1부터"+a+"까지의 합은"+sum+"입니다.");
			
			
	}

}
