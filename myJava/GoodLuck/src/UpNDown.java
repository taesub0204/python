import java.util.Random;
import java.util.Scanner;

public class UpNDown {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		
		
		Random r = new Random();		
		Scanner s = new Scanner(System.in);
		
		int com = r.nextInt(100)+1; // 100포함 안하고 99까지인데 1을 더해야함
		//System.out.print(com);
		int my;
		int count = 0;
		
		while(true) 
		{
			my =s.nextInt();
			count++;
			if(my == com) {
				System.out.print(count+"번만에 맞추었고 정답입니다.");
				break;
			}
			else if(my <com)
			{
				System.out.print("더 커야대");
			}
			else
			{
				System.out.print("더 작아해");
			}
			
		}
		

	}

}
