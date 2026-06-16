import java.util.Scanner;

public class Sample6 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		// 어떤 문자열 입력 받았을 대 공백을 제외한 단어수와 글자수를 출력하는 코드를 작성해 보자.
		// hi my name is kkk => 5 13
		
		
		Scanner sc = new Scanner(System.in);
		String str = sc.nextLine(); // next()한단어 만입력됨
		int charCnt = 0;
		int wordCnt = 1;
		for(int i = 0; i < str.length(); i++) {
			if(str.charAt(i)!=' ')
			{
				charCnt++;
			}
			else
			{
				wordCnt++;
			}
		}
		
		
		System.out.println("글자수는 " + charCnt);
		System.out.println("단어수는 " + wordCnt);
		

	}

}

//Pc 가위 바위 보
