public class ArraySort {

    public static void main(String[] args) {
        int[] num = {40, 50, 12, 43, 11, 9, 10, 90, 33, 76};
        
        
        
        // 1. 선택 정렬 (오름차순)
        for(int i = 0 ; i < num.length-1; i++) {
            int minIndex = i;
            for(int j = i+1; j < num.length; j++) {
                if(num[minIndex] > num[j])
                    minIndex = j;
            }
            if(i != minIndex) {
                int temp = num[i];
                num[i] = num[minIndex];
                num[minIndex] = temp;
            }
        }
        
        // 2. 오름차순 출력
        for (int n: num) System.out.print(n + "  ");
        System.out.println(); // 줄바꿈 추가
        
        // 3. 내림차순으로 뒤집기 (배열의 절반만큼 반복)
        for (int i = 0; i < num.length/2; i++) {
            int temp = num[i];
            num[i] = num[num.length-1-i];
            num[num.length-1-i] = temp;
        }
        
        // 4. 최종 결과 출력 (main 메서드 내부로 이동)
        for (int n: num)
            System.out.print(n + "  ");
    }
}