public class Test6 {
    public static void main(String[] args)
    {
        int c = 1;
        switch(3)
        {
            case 1: c += 3;
            case 2: c++;
            case 3: c = 0;
            case 4: c += 3;
            case 5: c -= 10;
            default: c--;

        }
        System.out.printf("%d",c);
    }
}
