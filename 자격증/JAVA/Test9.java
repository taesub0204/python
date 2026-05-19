public class Test9 {
    public static void main(String [] args)
    {
        int s, el = 0;
        for(int i = 6; i <= 30; i++){
            s = 0;
            // 1. 안쪽 루프에서는 오직 i의 약수를 찾아 s에 더하는 역할만 합니다.
            for(int j = 1; j <= i / 2; j++){
                if(i % j == 0) {
                    s = s + j;
                }
            } // 안쪽 j 반복문 끝
            
            // 2. 약수를 모두 더한 최종 결과(s)가 i와 같은지 '바깥쪽'에서 검사합니다.
            if(s == i) {
                el++;
            }
        } // 바깥쪽 i 반복문 끝
        
        System.out.printf("%d", el); // 출력 결과: 2
    }
}