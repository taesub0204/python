import java.util.Arrays;
import java.util.HashSet;
import java.util.Iterator;

public class Sample {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		HashSet<String> set1 = new HashSet<>();
		set1.add("apple");
		set1.add("banana");
		set1.add("apple");
		
		HashSet<String> set2 = new HashSet<>(Arrays.asList("H","e","home","dog"));   // 순서가 없다, 중복허용 하지 않아  집합 자료형  인덱스를 사용할 수 없음
		
		System.out.println(set1);
		System.out.println(set2);
		if(set1.contains("banana"))
			System.out.println("있다");
		else
			System.out.println("없다");
		
		
		set1.remove("banana");
		System.out.println(set1);
		
		for(String val : set2)
			System.out.println(val + "\t");
		
		Iterator<String> it = set1.iterator();   // 이터레이터는 참조 하나씩 하나씩 여기 만들어진 포문 처럼 set1 요소 하나하나 순회 하면서 링크드 리스트 안됨.... 
		while(it.hasNext())
			System.out.print(it.next()+"\t");
		
		System.out.print(set1.size());
		

	}

}
