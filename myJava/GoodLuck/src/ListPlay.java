import java.util.ArrayList;
import java.util.Comparator;
import java.util.Scanner;


public class ListPlay {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

		
//		ArrayList<Integer> num = new ArrayList<>();
//		ArrayList<String> str = new ArrayList<>();
//		
//		// Double, String, Integer, Character, Boolean, Float, 객체
//		num.add(10);
//		num.add(20);
//		num.add(0,100);
//		//num.remove(0);
//		//num.remove(Integer.valueOf(20));
//		num.set(0, 50);
//		num.clear();
//		
//		str.add("java");
//		str.add("program");
//		str.add("!!!");
//		str.add(2,"Fighting");
//		
//		
//		for (String s : str)
//			System.out.print(s + "  ");
//		
//		System.out.println();
//		for (int n : num)
//			System.out.print(n + "  ");
//		System.out.println();
//		
//		
//		
//		System.out.println(num.size());
//		System.out.println(str.size());
//	

		
		ArrayList<Integer> list = new ArrayList<>();
		list.add(70);
		list.add(85);
		list.add(40);
		list.add(12);
		System.out.println("초기리스트 :"+list);
		Scanner sin = new Scanner(System.in);
		
		
		while(true) {
		String signal = sin.next();
		
		if(signal.equals("i"))
		{
			int pos = sin.nextInt();
			int val = sin.nextInt();
			if(!list.contains(val))
			list.add(pos, val);
			System.out.println("삽입 후 :" + list);
			
		}
		
		else if(signal.equals("d"))
		{
			int del = sin.nextInt();
			if(list.contains(del))
				list.remove(Integer.valueOf(del));
			System.out.println("삭제 후 :"+list);
		}
		
		else if(signal.equals("e")) {
			System.out.println("BYe~~~");
			break;
		}
		
		else if(signal.equals("s"))
		{
			int val = sin.nextInt();
			int n = list.indexOf(val);
			if(n!=-1)
				System.out.println(n+1+"번째에 있어~~");
			else
				System.out.println("없어");
		}
		
		else if(signal.equals("m"))
		{
			int pos = sin.nextInt();
			int val = sin.nextInt();
			if(pos < list.size()) {
			list.set(pos,val);
			System.out.println("수정 후 :"+list);}
			else {
				System.out.println("해당위치가 존재하지 않아!!");
			}
		}
		
		else if(signal.equals("id")) {
			int val = sin.nextInt();
			int n = list.indexOf(val);
			if(n == -1)
				list.add(val);
			else
				list.remove(n);
			System.out.println("삽입 or 삭제 후 " + list);
			
		}
			
		

		
		
		

	
		
		
	//	list.add(2,100);
	//	System.out.println("삽입 후 :"+list);
		}
	
		
		list.sort(Comparator.naturalOrder());
		System.out.println("오름차순 정렬 후 : "+list);
		System.out.println("젤 작은 값 :" + list.get(0));
		
		
		list.sort(Comparator.reverseOrder());
		System.out.println("오름차순 정렬 후 : "+list);
		System.out.println("젤 큰 값 :" + list.get(0));
		

		
		
		
		
		
	}

}
