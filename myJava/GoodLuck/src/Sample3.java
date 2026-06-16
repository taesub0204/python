import java.util.ArrayList;
import java.util.Arrays;

public class Sample3 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		
		
		String num = "123";
		int n = Integer.parseInt(num);
		int s = n + 1;
		System.out.println(s);
		String str = n + ""; //문자열로 바뀜
		System.out.println(str);
		
		int n1 = 12345;
		String num1 = String.valueOf(n1); // 문자열을 정수
		String num2 = Integer.toString(n1); // 정수를 문자열
		
		String str1 = "123.234";
		double d = Double.parseDouble(str1);
		double dd = d + 1;
		System.out.println(dd);

		// 형변환 가능 다양한 함수 있음
		
		
		double d1 = n1; // 정수값을 굳이 변환이 필요 없어 매서드가 없음
		System.out.println(d1);
		
		final int N = 123; // 상수 한번 정해진 값 더이상 다른값으로 설정 불가
		final ArrayList<String> list = new ArrayList<>(Arrays.asList("aa","bb")); // final을 쓰면 객체 사용불가, 재설정 불가
		//list = new ArrayList<>(Arrays.asList("cc","dd"));		
		list.add("cc");
		
		System.out.println(list);
		
		

	}

}
